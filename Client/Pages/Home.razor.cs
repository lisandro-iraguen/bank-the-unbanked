using Client.Shared;
using Data.Wallet;
using Microsoft.AspNetCore.Components;
using CardanoSharp.Wallet.Extensions;
using Utils;
using CardanoSharp.Wallet.TransactionBuilding;
using CardanoSharp.Wallet.Models.Addresses;
using CardanoSharp.Wallet.Models.Transactions;
using CardanoSharp.Wallet.Extensions.Models.Transactions.TransactionWitnesses;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet;
using CardanoSharp.Wallet.Extensions.Models;
using Newtonsoft.Json;
using System.Text;
using CardanoSharp.Wallet.Extensions.Models.Transactions;


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
        private string PolicyAssetsID = null;        
        private ulong valueToTransfer = 100000000;
        private string walletfromTransfer;
        private string walletToTransfer;
        private bool isConecting = false;
        private WalletExtensionState walletState = null;
        protected override void OnInitialized()
        {

            _actionCommingFromTheMainLayout.Action += LoadWalletParametersWrapper;
            AssetsID = _configuration.GetValue<string>("AppSettings:AssetId");
            walletfromTransfer = _configuration.GetValue<string>("AppSettings:TestWalletFromTransfer");
            walletToTransfer = _configuration.GetValue<string>("AppSettings:TestWalletToSTransfer");
            PolicyAssetsID = _configuration.GetValue<string>("Policy:AssetId");

            if (AssetsID is null)
            {
                throw new Exception("AssetID cannot be null");
            }
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
            isConecting = false;
        }


        void OnChangeWalletAdress(string value, string name)
        {
            Console.WriteLine($"{name} value changed to {value}");
        }


        private async Task singTransaction()
        {
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



           


        }

    }
}
