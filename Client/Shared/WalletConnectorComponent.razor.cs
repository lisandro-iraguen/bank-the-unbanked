using Blazored.LocalStorage;
using Client.State.Wallet;
using Client.State.WalletConnector;
using Data.Wallet;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using static System.Net.WebRequestMethods;


namespace Client.Shared
{
    public partial class WalletConnectorComponent
    {

        [Inject] protected DialogService _dialogService { get; set; }    
        [Inject] protected IJSRuntime? _javascriptRuntime { get; set; }
        [Inject] protected ILocalStorageService _localStorage { get; set; }
        [Inject] protected IDispatcher dispatcher { get; set; }
        [Inject] protected IState<WalletConectorState> walletConectorState { get; set; }

      



        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new WalletInitializerAction(_javascriptRuntime));            
        }
     
        public void ConnectWalletAsync(string key)
        {
            dispatcher.Dispatch(new WalletConnectorAction(key, walletConectorState.Value.Extensions, _dialogService));
            
        }

        public async Task NavigateToNewTab(string url)
        {
            await _javascriptRuntime.InvokeAsync<object>("open", url, "_blank");
        }



    }



}
