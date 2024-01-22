using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Extensions.Models.Transactions.TransactionWitnesses;
using CardanoSharp.Wallet.Models.Addresses;
using CardanoSharp.Wallet.Models.Transactions;
using Client.Shared;
using Data.Oracle;
using Data.Wallet;
using Data.Web;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Xml.Linq;
using Utils;


namespace Client.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected IConfiguration _configuration { get; set; }

        [Inject]
        protected HttpClient http { get; set; }

        [CascadingParameter]
        private ActionWrapper _actionCommingFromTheMainLayout { get; set; }

        private string AssetsID = null;
        private ulong valueToTransfer = 100000000;
        private string walletfromTransfer;
        private string walletToTransfer;
        private string PolicyAssetsID = null;
        private string symbol;
        private string balanceAda;
        private string networkType;
        private int balancePesos=10000;
        private bool isConecting = false;
        private bool isSendingTransaction = false;

        private WalletExtensionState walletState;


        private CriptoDTO cryotpDto;
        protected override void OnInitialized()
        {

            _actionCommingFromTheMainLayout.Action += LoadWalletParametersWrapper;
            AssetsID = _configuration.GetValue<string>("AppSettings:AssetId");
            PolicyAssetsID = _configuration.GetValue<string>("Policy:AssetId");


           

            if (AssetsID is null)
            {
                throw new Exception("AssetID cannot be null");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            cryotpDto = await http.GetFromJsonAsync<CriptoDTO>("api/BinanceP2P");

            cryotpDto.DTTime = new DateTime(cryotpDto.Time);


        }
        public void LoadWalletParametersWrapper()
        {
            _ = LoadWalletParametersAsync();
        }
        public async Task LoadWalletParametersAsync()
        {
            isConecting = true;
            walletState = WalletSingleton.Instance.walletInstance;
            var walletConector = WalletSingleton.Instance._walletConnector;

            if (walletConector != null && walletConector.Initialized)
            {
                if (walletConector.Connected)
                {
                    var addressResult = await walletConector.GetUsedAddressesHex();
                    if (addressResult.Length > 0)
                    {

                        try
                        {
                            var adress = new Address(addressResult[0].HexToByteArray());                            
                            walletfromTransfer= adress.ToString();

                        }catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        walletfromTransfer = "no hay wallets cargadas";
                    }

                    var result = await walletConector.GetBalance();
                    Console.WriteLine("Balance:");
                    Console.WriteLine($" - Coin: {result.Coin}");
                    foreach (var asset in result.MultiAsset)
                    {
                        var policyId = asset.Key.ToStringHex();
                        Console.WriteLine(($" - Policy: {policyId}"));

                        foreach (var token in asset.Value.Token)
                        {

                            var assetName = token.Key.ToStringHex();
                            assetName = ComponentUtils.HexToString(assetName);
                            Console.WriteLine(($"   - AssetName: {assetName}"));
                            Console.WriteLine(($"   - Tokens: {token.Value}"));
                        }
                    }

                }
            }
            balanceAda = walletState.BalanceAda;
            networkType = walletState.Network.ToString();
            symbol = walletState.CoinCurrency;
           
            var auxAddress = walletState.UsedAdress[0];
            try
            {
                if (!string.IsNullOrEmpty(auxAddress))
                {
                    walletfromTransfer = auxAddress.ToAddress().ToStringHex();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            isConecting = false;
            StateHasChanged();
        }


        void OnChangeWalletAdress(string value, string name)
        {
            Console.WriteLine($"{name} value changed to {value}");
        }


        private async Task singTransaction()
        {
            isSendingTransaction = true;
            walletState = WalletSingleton.Instance.walletInstance;
            var walletConector = WalletSingleton.Instance._walletConnector;
            string url = $"/api/TxBuild?walletFrom={walletfromTransfer}&walletTo={walletToTransfer}&value={valueToTransfer}";
            var response = await http.GetAsync(url);
            var txSignData = new TxRequest();
            if (response.IsSuccessStatusCode)
            {
                var transaction = await response.Content.ReadFromJsonAsync<Transaction>();
                txSignData.transactionCbor = transaction.Serialize().ToStringHex();
                var witnessSet = await walletConector.SignTx(transaction, true);
                txSignData.witness = witnessSet;
                Console.WriteLine($"Success: transaction");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");

            }



            string jsonContent = JsonConvert.SerializeObject(txSignData);
            StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response2 = await http.PostAsync("api/TxSign", content);
            if (response.IsSuccessStatusCode)
            {
                var result = await response2.Content.ReadFromJsonAsync<Transaction>();
                var delivered = await walletConector.SubmitTx(result);
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }





            isSendingTransaction = false;
        }

    }
}
