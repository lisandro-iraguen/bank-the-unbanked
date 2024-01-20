

using CardanoSharp.Wallet.CIPs.CIP30.Models;
using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Models.Transactions;
using Data.Exceptions;
using PeterO.Cbor2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Components;
using Blazored.LocalStorage;
using Utils;
using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using CardanoSharp.Wallet.Models;
using CardanoSharp.Wallet.Extensions.Models;

namespace Data.Wallet
{
    public class WalletConnector
    {

        public List<WalletExtensionState>? _wallets = null;
        public WalletConnectorJsInterop? _walletConnectorJs;
        public WalletExtensionState? _connectedWallet { get; set; }

        public List<WalletExtension> SupportedExtensions { get; set; }

        public bool Connecting { get; private set; }
        public bool Initialized { get; private set; }
        public bool Connected { get; private set; }

        private ILocalStorageService _localStorage;
        private HttpClient _http;
        private IJSRuntime _javascriptRuntime;
        public WalletConnector(ILocalStorageService lStorage, HttpClient client, IJSRuntime jSRuntime)
        {
            _localStorage = lStorage;
            _http = client;
            _javascriptRuntime = jSRuntime;
        }
        public async Task IntializedWalletAsync()
        {
            try
            {
                IEnumerable<WalletExtension> walletExtensions = await _http.GetFromJsonAsync<IEnumerable<WalletExtension>>("api/WalletsData");
                SupportedExtensions = walletExtensions.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if (_walletConnectorJs == null)
            {
                _walletConnectorJs = new WalletConnectorJsInterop(_javascriptRuntime);
            }
            _wallets = await _walletConnectorJs.Init(SupportedExtensions);

        }

        public async Task ConnectWalletAutomatically()
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
        }
        public async Task ConnectWallet(string key)
        {
            var supportedWalletKeys = SupportedExtensions?.Select(s => s.Key)?.ToArray();

            if (supportedWalletKeys != null && supportedWalletKeys.Length > 0)
            {
                var storedWalletKey = await GetStoredWalletKeyAsync(supportedWalletKeys);
                if (string.IsNullOrWhiteSpace(storedWalletKey))
                {
                    if (supportedWalletKeys.Contains(key))
                    {
                        if (!await ConnectWalletAsync(key, false))
                        {
                            await RemoveStoredWalletKeyAsync();
                        }
                    }
                }
            }
        }


        private async Task<string> GetStoredWalletKeyAsync(params string[] supportedWalletKeys)
        {
            var result = string.Empty;

            try
            {
                if (_localStorage != null && supportedWalletKeys != null)
                {
                    var walletKey = await _localStorage.GetItemAsStringAsync(ComponentUtils.ConnectedWalletKey);

                    if (!string.IsNullOrWhiteSpace(walletKey) && supportedWalletKeys.Any(w => w.Equals(walletKey, StringComparison.OrdinalIgnoreCase)))
                    {
                        result = walletKey;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            return result;
        }
        private async Task RemoveStoredWalletKeyAsync()
        {
            if (_localStorage != null)
            {
                await _localStorage.RemoveItemAsync(ComponentUtils.ConnectedWalletKey);
            }
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
                _connectedWallet.UsedAdress = await GetUsedAddressesHex();
            }
            _connectedWallet!.Network = await GetNetworkType();
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
        public async ValueTask<bool> ConnectWalletAsync(string walletKey, bool suppressEvent = false)
        {
            Initialized = true;
            try
            {

                Connecting = true;
                var result = await _walletConnectorJs!.ConnectWallet(walletKey);
                if (result)
                {
                    Connected = true;

                    _connectedWallet = _wallets!.First(x => x.Key == walletKey);
                    await RefreshConnectedWallet();
                    _connectedWallet.Connected = Connected;
                    _connectedWallet.WalletConnectorJs = _walletConnectorJs;
                    await SetStoredWalletKeyAsync(walletKey);
                    WalletSingleton.Instance.walletInstance = _connectedWallet;

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
            }
            return false;
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

       
        public async ValueTask<string[]> GetUsedAddressesHex(Paginate? paginate = null)
        {
            var addresses = await _walletConnectorJs!.GetUsedAddresses(paginate);
            foreach (var address in addresses)
            {
                Console.WriteLine($"USED ADDRESS: {address}");
            }
            return addresses;
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

        public async ValueTask<DataSignature> SignData(string address, string hexData)
        {
            CheckInitializedAndConnected();
            return await _walletConnectorJs!.SignData(address, hexData);
        }

        public async ValueTask<string> SignTx(Transaction tx, bool partialSign = false)
        {
            var txCbor = tx.Serialize().ToStringHex();
            return await SignTxCbor(txCbor, partialSign);
        }
        public async ValueTask<string> SignTxCbor(string txCbor, bool partialSign = false)
        {
            CheckInitializedAndConnected();
            Console.WriteLine($"TX CBOR: {txCbor}");
            return await _walletConnectorJs!.SignTx(txCbor, partialSign);
        }

        public async ValueTask<string> SubmitTx(Transaction tx)
        {
            var txCbor = tx.Serialize().ToStringHex();
            return await SubmitTxCbor(txCbor);
        }
        public async ValueTask<string> SubmitTxCbor(string txCbor)
        {
            CheckInitializedAndConnected();
            Console.WriteLine(($"TX CBOR: {txCbor}"));
            return await _walletConnectorJs!.SubmitTx(txCbor);
        }
        private async Task SetStoredWalletKeyAsync(string walletKey)
        {
            if (_localStorage != null && !string.IsNullOrWhiteSpace(walletKey))
            {
                await _localStorage.SetItemAsStringAsync(ComponentUtils.ConnectedWalletKey, walletKey);
            }
        }
        public async ValueTask DisconnectWalletAsync(bool suppressEvent = false)
        {
            await RemoveStoredWalletKeyAsync();
            await _walletConnectorJs!.DisposeAsync();
            while (_wallets!.Any(x => x.Connected))
            {
                _wallets!.First(x => x.Connected).Connected = false;
            }

            return;
        }

        public async ValueTask<Utxo[]> GetUtxos(TransactionOutputValue? requiredOutput = null, Paginate? paginate = null)
        {
            string? amountCbor = null;
            if (requiredOutput != null)
            {
                amountCbor = requiredOutput.Serialize().ToStringHex();
            }
            var utxoCbors = await GetUtxosCbor(amountCbor, paginate);
            var utxoList = new List<Utxo>();
            foreach (var utxoCbor in utxoCbors)
            {
                try
                {
                    utxoList.Add(utxoCbor.HexToByteArray().DeserializeUtxo());
                }
                catch (Exception ex)
                {

                    throw new Exception($"error during utxo deserialization {ex}{utxoCbor}");
                }
            }
            return utxoList.ToArray();
        }

        public async ValueTask<string[]> GetUtxosCbor(string? requiredOutputCbor = null, Paginate? paginate = null)
        {
            CheckInitializedAndConnected();
            var utxoCbors = await _walletConnectorJs!.GetUtxos(requiredOutputCbor, paginate);
            foreach (var utxoCbor in utxoCbors)
            {
                Console.WriteLine($"UTXO CBOR: {utxoCbor}");
            }
            return utxoCbors;
        }

        public void disconectWallet()
        {
            Task.Run(async () =>
            {
                await RemoveStoredWalletKeyAsync();
            });
            _connectedWallet = null;
        }
    }
}
