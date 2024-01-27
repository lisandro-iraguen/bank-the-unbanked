using Client.State.Crypto;
using Client.State.Wallet;
using Client.State.WalletConnecting;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;


namespace Client.Pages
{
    public partial class Home : FluxorComponent
    {
        [Inject] protected IConfiguration _configuration { get; set; }
        [Inject] protected IDispatcher dispatcher { get; set; }
        
        [Inject] IState<CryptoState> cryptoState { get; set; }
        [Inject] IState<WalletState> walletState { get; set; }
        [Inject] IState<WalletConnectingState> walletConectingState { get; set; }


        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new FetchCryptoAction());
        }



        void OnChangeWalletAdress(string value, string name)
        {
            Console.WriteLine($"{name} value changed to {value}");
        }


        private async Task singTransaction()
        {
            //isSendingTransaction = true;
            //walletState = WalletSingleton.Instance.walletInstance;
            //var walletConector = WalletSingleton.Instance._walletConnector;
            //string url = $"/api/TxBuild?walletFrom={walletfromTransfer}&walletTo={walletToTransfer}&value={valueToTransfer}";
            //var response = await http.GetAsync(url);
            //var txSignData = new TxRequest();
            //if (response.IsSuccessStatusCode)
            //{
            //    var transaction = await response.Content.ReadFromJsonAsync<Transaction>();
            //    txSignData.transactionCbor = transaction.Serialize().ToStringHex();
            //    //var witnessSet = await walletConector.SignTx(transaction, true);
            //    //txSignData.witness = witnessSet;
            //    Console.WriteLine($"Success: transaction");
            //}
            //else
            //{
            //    Console.WriteLine($"Error: {response.StatusCode}");

            //}



            //string jsonContent = JsonConvert.SerializeObject(txSignData);
            //StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            //HttpResponseMessage response2 = await http.PostAsync("api/TxSign", content);
            //if (response.IsSuccessStatusCode)
            //{
            //    var result = await response2.Content.ReadFromJsonAsync<Transaction>();
            //    //var delivered = await walletConector.SubmitTx(result);
            //}
            //else
            //{
            //    Console.WriteLine($"Error: {response.StatusCode}");
            //}





            //isSendingTransaction = false;
        }

    }
}
