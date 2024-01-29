using Client.State.Connection;
using Client.State.Crypto;
using Client.State.Transaction;
using Client.State.Wallet;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Utils;



namespace Client.Pages
{
    public partial class Home : FluxorComponent
    {
        [Inject] protected IConfiguration _configuration { get; set; }
        [Inject] protected IDispatcher dispatcher { get; set; }
        [Inject] IState<CryptoState> cryptoState { get; set; }
        [Inject] IState<WalletState> walletState { get; set; }
        [Inject] IState<ConectedState> walletConecting { get; set; }
        [Inject] IState<TransactionState> transactionState { get; set; }




        private string walletToTransfer;
        private int valueToTransfer = 1000000;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new FetchCryptoAction());


            walletToTransfer = "addr_test1qpx48ss8fkyuujvyrtrxlt4jv8pscslzvw6yvz68lt2gyj2yaakargznpqxp22n49ysqdlwqeuh8cdvj4heyksvuj2nshzyk62";
            
        }



        void OnChangeWalletAdress(string value, string name)
        {
            Console.WriteLine($"{name} value changed to {value}");
        }



        private async Task SignAndSubmitTransaction()
        {
            dispatcher.Dispatch(new SignTransactionAction(walletState.Value.Wallet, walletToTransfer, valueToTransfer));

        }

        public string hexToString(string hexString)
        {
            return ComponentUtils.HexToString(hexString);
        }
    }
}
