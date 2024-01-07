using Client.Shared;
using Data.Wallet;
using Microsoft.AspNetCore.Components;
using CardanoSharp.Wallet.Extensions;
using Utils;
using CardanoSharp.Wallet.TransactionBuilding;
using CardanoSharp.Wallet.Models.Addresses;


namespace Client.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected IConfiguration _configuration { get; set; }

        [CascadingParameter]
        private ActionWrapper _actionCommingFromTheMainLayout { get; set; }

        private string walletMessage = null;
        private string symbol = null;
        private string symbolArs = null;
        private string balanceAda = null;
        private string balanceArs = null;
        private string AssetsID = null;
        private string PolicyAssetsID = null;
        private string networkType = null;

        private ulong valueToTransfer = 0;
        private string walletToSTransfer;
        private bool isConecting = false;
        private WalletExtensionState walletState = null;
        protected override void OnInitialized()
        {

            _actionCommingFromTheMainLayout.Action += LoadWalletParametersWrapper;
            AssetsID = _configuration.GetValue<string>("AppSettings:AssetId");
            PolicyAssetsID = _configuration.GetValue<string>("AppSettings:PolicyAssetId");
            walletToSTransfer = _configuration.GetValue<string>("AppSettings:TestWalletToSTransfer");

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
         
         }


        void OnChangeWalletAdress(string value, string name)
        {
            Console.WriteLine($"{name} value changed to {value}");
        }

     
        private async Task singTransaction()
        {
            walletState = WalletSingleton.Instance.walletInstance;
            var walletConector = WalletSingleton.Instance._walletConnector;
            string usedWallet = walletState.UsedAdress.First();
            var tokenHex = AssetsID;
            var policyId = PolicyAssetsID;
            var tokenName = ComponentUtils.HexToString(AssetsID);
            string imputTx = "";
            uint indexTx = 0;

            Address baseAddr = new Address(walletToSTransfer);

            int currentSlot = await walletConector.GetNetworkSlot();

            var utxo = await walletConector.GetUtxos();
            var u = utxo.Last();
            imputTx = u.TxHash;
            indexTx = u.TxIndex;

            uint tokenQuantity = 1;
            //var tokenAsset = TokenBundleBuilder.Create
            //    .AddToken(policyId.ToBytes(), tokenName.ToBytes(), tokenQuantity);

            //var transactionBody = TransactionBodyBuilder.Create
            //                     .AddInput(imputTx, indexTx)
            //                     .AddOutput(baseAddr, valueToTransfer)
            //                     .SetTtl(currentSlot + 1000)
            //                     .SetFee(0);


            //CardanoSharp.Wallet.Models.Transactions.Transaction transaction = null;
            //try
            //{
            //    transaction = TransactionBuilder.Create
            //    .SetBody(transactionBody)
            //    .Build();

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("build transaction'failed");
            //    Console.WriteLine(ex.Message);
            //}



            //try
            //{

            //    var finalResult = await walletConector.SignTx(transaction);
            //    Console.WriteLine($"Sign TX (witness set cbor):");
            //    Console.WriteLine(finalResult);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Sign TX transaction failed");
            //    Console.WriteLine(ex.Message);
            //}


        }

    }
}
