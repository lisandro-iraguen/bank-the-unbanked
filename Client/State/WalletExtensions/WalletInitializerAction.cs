using Blazored.LocalStorage;
using Components;
using Microsoft.JSInterop;
using Radzen;

namespace Client.State.WalletExtensions
{
    public class WalletInitializerAction
    {
        public IJSRuntime? JsRuntime { get; }
        public ILocalStorageService? LocalStorageSerivce { get; }
        public DialogService? DialogService { get; }
        private WalletInitializerAction() { }
        public WalletInitializerAction(IJSRuntime jSRuntime, ILocalStorageService localStorage, DialogService dialogService)
        {

            JsRuntime = jSRuntime;
            LocalStorageSerivce = localStorage;
            DialogService = dialogService;
        }
    }
}
