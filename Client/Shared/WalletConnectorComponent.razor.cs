using Blazored.LocalStorage;
using Client.State.Wallet.Extension;
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
        [Inject] protected HttpClient http { get; set; }
        [Inject] protected IJSRuntime? _javascriptRuntime { get; set; }
        [Inject] protected ILocalStorageService _localStorage { get; set; }
        [Inject] protected IDispatcher dispatcher { get; set; }
        [Inject] protected IState<WalletState> walletState { get; set; }

        private WalletConnector _walletConector;
        


        protected override async Task OnInitializedAsync()
        {
            _walletConector = new WalletConnector(_localStorage, http, _javascriptRuntime);
            WalletSingleton.Instance._walletConnector= _walletConector;          
            await InitializePersistedWalletAsync();
        }
        private async Task InitializePersistedWalletAsync()
        {
            await _walletConector.IntializedWalletAsync();
            StateHasChanged();
        }
        public async ValueTask ConnectWalletAsync(string key)
        {
            //await _walletConector.ConnectWallet(key);

            
            _dialogService.Close();
        
        }

            public async Task NavigateToNewTab(string url)
        {
            await _javascriptRuntime.InvokeAsync<object>("open", url, "_blank");
        }



    }



}
