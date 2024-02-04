using Blazored.LocalStorage;
using Client.State.Connection;
using Client.State.Wallet;
using Client.State.WalletExtensions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;


namespace Client.Shared
{
    public partial class WalletConnectorComponent
    {

        [Inject] protected DialogService? _dialogService { get; set; }    
        [Inject] protected IJSRuntime? _javascriptRuntime { get; set; }
        [Inject] protected ILocalStorageService? _localStorage { get; set; }
        [Inject] protected IDispatcher? dispatcher { get; set; }
        [Inject] protected IState<WalletExtensionsState>? walletConectorState { get; set; }

      



        protected override void OnInitialized()
        {
            base.OnInitialized();
                     
        }
     
        public void ConnectWalletAsync(string key)
        {
            dispatcher.Dispatch(new WalletConnectorAction(key, walletConectorState.Value.Extensions, _dialogService, _localStorage));            
        }

        public async Task NavigateToNewTab(string url)
        {
            await _javascriptRuntime.InvokeAsync<object>("open", url, "_blank");
        }



    }



}
