using Client.Shared;
using Client.State.Balance;
using Client.State.Connection;
using Client.State.Crypto;
using Client.State.Transaction;
using Client.State.TransactionFee;
using Client.State.Wallet;
using Client.State.WalletExtensions;
using Client.State.WalletHistory;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using Radzen;
using Utils;



namespace Client.Pages
{
    public partial class Home : FluxorComponent
    {
        [Inject] protected IConfiguration? _configuration { get; set; }
        [Inject] protected IDispatcher? dispatcher { get; set; }
        [Inject] IState<CryptoState>? cryptoState { get; set; }
        [Inject] IState<WalletExtensions>? walletState { get; set; }
        [Inject] IState<ConectedState>? walletConecting { get; set; }
        [Inject] IState<TransactionState>? transactionState { get; set; }
        [Inject] IState<TransactionFeeState>? transactionFeeState { get; set; }
        //[Inject] IState<WalletHistoryState>? walletHistoryState { get; set; }
        [Inject] IState<BalanceState>? balanceState { get; set; }
        [Inject] protected DialogService? _dialogService { get; set; }
        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }



        private I18nText.Web? webText;
        private string? walletToTransfer;
        private ulong valueToTransfer = 0;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new FetchCryptoAction());
            walletToTransfer = "";
        }

        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);

        }

        void OnChangeWalletAdress(string value, string name)
        {
            Console.WriteLine($"{name} value changed to {value}");

        }

        void OnChangeValueToBeTransfer(ulong value, string name)
        {
            Console.WriteLine($"{name} to be transfer changed to {value}");
            GetTransactionFee();

        }

        private Task GetTransactionFee()
        {
            ulong adaToTransfer = valueToTransfer / cryptoState.Value.Crypto.TotalBid;
            dispatcher.Dispatch(new TransactionFeeAction(walletState.Value.Wallet, walletToTransfer, adaToTransfer));
            return Task.CompletedTask;
        }

        private async Task SignAndSubmitTransaction()
        {
            ulong adaToTransfer = valueToTransfer / cryptoState.Value.Crypto.TotalBid;
            dispatcher.Dispatch(new SignTransactionAction(walletState.Value.Wallet, walletToTransfer, adaToTransfer,webText.TransactionSuccess, webText.FiatSimbol+": "+ valueToTransfer.ToString()));
            await OpenTransactionPopUp();

        }

        private bool CantSendStransaction()
        {
            if (valueToTransfer == 0) return true;
            var valueInAda = valueToTransfer / cryptoState.Value.Crypto.TotalBid;
            if ((transactionFeeState.Value.Fee > valueInAda)) return true;
            if ((transactionFeeState.Value.Fee == 0)) return true;
            if (transactionState.Value.IsSigningTransaction) return true;
            if (transactionFeeState.Value.IsLoading) return true;
            if (valueInAda > walletState.Value.Wallet.Balance) return true;

            return false;
        }
        public async Task OpenTransactionPopUp()
        {
            var dialogResult = await _dialogService.OpenAsync(webText.ProcessingTransaction, RenderWalletConnector);

        }


        private RenderFragment RenderWalletConnector(DialogService service)
        {

            RenderFragment fragment = builder =>
            {
                builder.OpenComponent(0, typeof(TransactionPopUp));
                builder.CloseComponent();
            };

            return fragment;
        }
    }
}
