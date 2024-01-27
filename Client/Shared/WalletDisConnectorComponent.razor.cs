using Client.State.Wallet;
using Client.State.WalletExtensions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Shared
{
    public partial class WalletDisConnectorComponent
    {
        [Inject]
        protected DialogService _dialogService { get; set; }
        [Inject] protected IState<WalletState> walletState { get; set; }
        [Inject] protected IState<WalletExtensionsState> walletConectorState { get; set; }
        [Inject] protected IDispatcher dispatcher { get; set; }


        public async ValueTask DisconnectWalletAsync(bool suppressEvent = false)
        {
            dispatcher.Dispatch(new WalletDisconectAction());
           _dialogService.Close();
           StateHasChanged();
           return;
        }



    }



}
