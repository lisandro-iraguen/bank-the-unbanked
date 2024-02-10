using Blazored.LocalStorage;
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
        protected DialogService? _dialogService { get; set; }
        [Inject] protected IState<WalletExtensions>? walletState { get; set; }
        [Inject] protected IState<WalletExtensionsState>? walletConectorState { get; set; }
        [Inject] protected IDispatcher? dispatcher { get; set; }
        [Inject] protected ILocalStorageService? localStorage{ get; set; }


        public ValueTask DisconnectWalletAsync(bool suppressEvent = false)
        {
            dispatcher.Dispatch(new WalletDisconectAction(walletState.Value.Wallet, localStorage));
           _dialogService.Close();
            return new ValueTask();
        }
    }



}
