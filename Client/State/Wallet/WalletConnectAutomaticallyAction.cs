using Blazored.LocalStorage;
using Components;
using Data.Wallet;
using Fluxor;
using Microsoft.JSInterop;
using Radzen;

namespace Client.State.Wallet
{
    public class WalletConnectAutomaticallyAction
    {   
        public IEnumerable<WalletExtensionState> Wallets { get; }
        public ILocalStorageService LocalStorageSerivce { get; }
        public IDispatcher Dispatcher { get; }
     

        private WalletConnectAutomaticallyAction() { }
        public WalletConnectAutomaticallyAction(IEnumerable<WalletExtensionState> wallets, ILocalStorageService localStorageSerivce)
        {            
            Wallets = wallets;
            LocalStorageSerivce = localStorageSerivce;            
        }
    }
}
