using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Models.Transactions;
using Client.Components;
using Client.Pages;
using Data.Exceptions;
using Data.Wallet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PeterO.Cbor2;
using Radzen;
using System.Net.Http.Json;
using Utils;
using static System.Net.WebRequestMethods;

namespace Client.Shared
{
    public partial class WalletConnectorComponent
    {
        [Inject]
        protected DialogService _dialogService { get; set; }

        [Inject]
        protected IJSRuntime _javascriptRuntime { get; set; }
        
        [Inject]
        protected HttpClient http { get; set; }
        

        public bool Connecting { get; private set; }

        private List<WalletExtensionState>? _wallets = null;
        private WalletConnectorJsInterop? _walletConnectorJs;
        public WalletExtensionState? _connectedWallet { get; private set; }

        public List<WalletExtension> SupportedExtensions { get; set; }
        public bool Initialized { get; private set; }
        public bool Connected { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IEnumerable<WalletExtension> walletExtensions = await http.GetFromJsonAsync<IEnumerable<WalletExtension>>("api/WalletsData");
                SupportedExtensions = walletExtensions.ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (_walletConnectorJs == null)
            {
                _walletConnectorJs = new WalletConnectorJsInterop(_javascriptRuntime);
            }
            //_selfReference = DotNetObjectReference.Create(this);
            _wallets = await _walletConnectorJs.Init(SupportedExtensions);
            await InitializePersistedWalletAsync();            
        }
        private async Task InitializePersistedWalletAsync()
        {

            var supportedWalletKeys = SupportedExtensions?.Select(s => s.Key)?.ToArray();

            if (supportedWalletKeys != null && supportedWalletKeys.Length > 0)
            {
                   var storedWalletKey = await GetStoredWalletKeyAsync(supportedWalletKeys);

                    if (!string.IsNullOrWhiteSpace(storedWalletKey))
                    {
                        if (!await ConnectWalletAsync(storedWalletKey, false))
                        {
                            await RemoveStoredWalletKeyAsync();
                        }
                    }
            }

            StateHasChanged();
        }


        public async ValueTask DisconnectWalletAsync(bool suppressEvent = false)
        {
            await _walletConnectorJs!.DisposeAsync();
            while (_wallets!.Any(x => x.Connected))
            {
                _wallets!.First(x => x.Connected).Connected = false;
            }
           
            return;
        }

        public async ValueTask<bool> ConnectWalletAsync(string walletKey, bool suppressEvent = false)
        {
            //await DisconnectWalletAsync(true).ConfigureAwait(false);
            Initialized = true;



            try
            {
         
                Connecting = true;
                StateHasChanged();

                var result = await _walletConnectorJs!.ConnectWallet(walletKey);
                if (result)
                {
                    Connected = true ;

                    _connectedWallet = _wallets!.First(x => x.Key == walletKey);
                    await RefreshConnectedWallet();
                    _connectedWallet.Connected = Connected;
                     
                     
                    WalletSingleton.Instance = _connectedWallet;                   
                }
             
                return result;
            }
            //suppress all errors as it could be valid user refusal
            //(cant get enough detail out of gero wallet to ensure specific handling)
            catch (ErrorCodeException ecex)
            {
                Console.WriteLine("Caught error code exception: " + ecex.Code + " - " + ecex.Info);
            }
            catch (PaginateException pex)
            {

                Console.WriteLine("Caught paginate exception: " + pex.MaxSize);
            }
            catch (WebWalletException wex)
            {

                Console.WriteLine("Caught web wallet exception: " + wex.Data.Keys.ToString());
            }
            catch (Exception ex)
            {

                Console.WriteLine("Caught exception: " + ex.Message);
            }
            finally
            {
                Connecting = false;
                StateHasChanged();
                _dialogService.Close();
            }
            return false;
        }

        public async Task NavigateToNewTab(string url)
        {           
            await _javascriptRuntime.InvokeAsync<object>("open", url, "_blank");
        }
        private async Task<string> GetStoredWalletKeyAsync(params string[] supportedWalletKeys)
        {
            var result = string.Empty;
            //TODO
            return result;
        }
        private async Task RemoveStoredWalletKeyAsync()
        {
            //TODO
        }

        public async ValueTask RefreshConnectedWallet()
        {
            var balance = await GetBalance();
            if (balance != null)
            {
                _connectedWallet!.TokenCount = 0;
                _connectedWallet.TokenPreservation = 0;
                _connectedWallet.NativeAssets = new();
                if (balance.MultiAsset != null && balance.MultiAsset.Count > 0)
                {
                    _connectedWallet.TokenPreservation = balance.MultiAsset.CalculateMinUtxoLovelace();
                    _connectedWallet.TokenCount = balance.MultiAsset.Sum(x => x.Value.Token.Keys.Count);
                    _connectedWallet.NativeAssets = balance.MultiAsset.SelectMany(policy =>
                           policy.Value.Token.Select(asset =>
                               new KeyValuePair<string, ulong>(
                                   $"{policy.Key.ToStringHex()}-{asset.Key.ToStringHex()}",
                                   (ulong)asset.Value)))
                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                }
                _connectedWallet.Balance = balance.Coin - _connectedWallet.TokenPreservation;
            }
            _connectedWallet!.Network = await GetNetworkType();
            StateHasChanged();
        }

        public async ValueTask<TransactionOutputValue> GetBalance()
        {
            var result = await GetBalanceCbor();
            var cborObj = CBORObject.DecodeFromBytes(result.HexToByteArray());
            if (cborObj.Type == CBORType.Integer)
            {
                var number = cborObj.AsNumber();
                var coin = number.ToUInt64Checked();
                return new TransactionOutputValue() { Coin = coin };
            }

            var outputValue = result.HexToByteArray().DeserializeTransactionOutputValue();
            return outputValue;
        }

        public async ValueTask<string> GetBalanceCbor()
        {
            CheckInitializedAndConnected();
            var result = await _walletConnectorJs!.GetBalance();
            Console.WriteLine($"BALANCE CBOR: {result}");
            return result;
        }
        public async ValueTask<NetworkType> GetNetworkType()
        {
            var networkId = await GetNetworkTypeId();
            return ComponentUtils.GetNetworkType(networkId);
        }
        public async ValueTask<int> GetNetworkTypeId()
        {
            CheckInitializedAndConnected();
            var networkId = await _walletConnectorJs!.GetNetworkId();
            Console.WriteLine($"NETWORK ID: {networkId}");
            return networkId;
        }
        private void CheckInitialized()
        {
            if (!Initialized || _walletConnectorJs == null)
            {
                throw new InvalidOperationException("Component not initialized");
            }
        }

        private void CheckInitializedAndConnected()
        {
            CheckInitialized();
            if (!Connected)
            {
                throw new InvalidOperationException("No wallet connected");
            }
        }

    }



}
