using Client.State.Crypto;
using Client.State.Wallet;
using Client.State.WalletHistory;
using Data.History;
using Fluxor;
using Microsoft.AspNetCore.Components;


namespace Client.Shared.Transaction
{

    public partial class TransactionHistory
    {
        [Inject] IState<Client.State.WalletHistory.WalletHistoryState>? walletHistoryState { get; set; }
        [Inject] IState<CryptoState>? cryptoState { get; set; }

        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }
        [Inject] protected IDispatcher? dispatcher { get; set; }
        [Inject] IState<WalletExtensions>? walletState { get; set; }


        private I18nText.Web? webText;


        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);         
            dispatcher.Dispatch(new WalletHistoryConnectorAction(walletState.Value.Wallet));
        }
    }
}
