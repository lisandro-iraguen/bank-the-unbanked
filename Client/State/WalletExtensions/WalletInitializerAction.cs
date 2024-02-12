using Blazored.LocalStorage;
using Components;
using Microsoft.JSInterop;
using Radzen;

namespace Client.State.WalletExtensions
{
    public class WalletInitializerAction
    {
        public IJSRuntime? JsRuntime { get; }
        
        public DialogService? DialogService { get; }
        private WalletInitializerAction() { }
        public WalletInitializerAction(IJSRuntime jSRuntime, DialogService dialogService)
        {

            JsRuntime = jSRuntime;
            DialogService = dialogService;
        }
    }
}
