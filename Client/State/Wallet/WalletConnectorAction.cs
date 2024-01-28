using Blazored.LocalStorage;
using Components;
using Data.Wallet;
using Microsoft.JSInterop;
using Radzen;

namespace Client.State.Wallet
{
    public class WalletConnectorAction
    {

        public string Key { get; }
        public IEnumerable<WalletExtensionState> Wallets { get; }
        public DialogService DialogService { get; }
        public ILocalStorageService LocalStorage{ get; }



        private WalletConnectorAction() { }
        public WalletConnectorAction(string key, IEnumerable<WalletExtensionState> wallets, DialogService _dialogService,ILocalStorageService localStorage)
        {
            Key = key;
            Wallets = wallets;
            DialogService = _dialogService;
            LocalStorage = localStorage;
        }
    }
}
