using Blazored.LocalStorage;
using Components;
using Data.Wallet;
using Microsoft.JSInterop;
using Radzen;

namespace Client.State.Wallet
{
    public class WalletConnectAutomaticallyAction
    {   
        public IEnumerable<WalletExtensionState> Wallets { get; }
        public ILocalStorageService LocalStorageSerivce { get; }
        public DialogService DialogService { get; }

        private WalletConnectAutomaticallyAction() { }
        public WalletConnectAutomaticallyAction(IEnumerable<WalletExtensionState> wallets, ILocalStorageService localStorageSerivce, DialogService dialogService)
        {            
            Wallets = wallets;
            LocalStorageSerivce = localStorageSerivce;
            DialogService = dialogService;
        }
    }
}
