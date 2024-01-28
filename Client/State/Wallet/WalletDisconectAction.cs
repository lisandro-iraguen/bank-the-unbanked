using Blazored.LocalStorage;
using Components;
using Data.Wallet;
using Microsoft.JSInterop;
using Radzen;

namespace Client.State.Wallet
{
    public class WalletDisconectAction
    {
     
        public WalletExtensionState Wallet { get; }        
        public ILocalStorageService LocalStorage { get; }

        public WalletDisconectAction(WalletExtensionState wallet, ILocalStorageService localStorage)
        {
            Wallet = wallet;
            LocalStorage = localStorage;
        }
    }
}
