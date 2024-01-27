using Data.Wallet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Components;
using Client.State.WalletConnector;
using Fluxor;
using Client.State.Wallet;

namespace Client.Shared
{
    public partial class WalletDisConnectorComponent
    {
        [Inject]
        protected DialogService _dialogService { get; set; }
        [Inject] protected IState<WalletState> walletState { get; set; }
        [Inject] protected IState<WalletConectorState> walletConectorState { get; set; }
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
