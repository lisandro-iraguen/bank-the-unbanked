using Blazored.LocalStorage;
using Client.State.Wallet;
using Client.State.WalletExtensions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Shared.Wallet
{
    public partial class WalletDisConnectorComponent
    {
        [Inject]
        protected DialogService? _dialogService { get; set; }
        [Inject] protected IState<WalletExtensions>? walletState { get; set; }
        [Inject] protected IState<WalletExtensionsState>? walletConectorState { get; set; }
        [Inject] protected IDispatcher? dispatcher { get; set; }

        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }



        private I18nText.Web? webText;

        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);

        }

        public ValueTask DisconnectWalletAsync(bool suppressEvent = false)
        {
            dispatcher.Dispatch(new WalletDisconectAction(walletState.Value.Wallet));
            _dialogService.Close();
            return new ValueTask();
        }
    }



}
