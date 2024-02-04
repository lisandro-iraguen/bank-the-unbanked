using Blazored.LocalStorage;
using Client.State.Connection;
using Client.State.Crypto;
using Client.State.Wallet;
using Client.State.WalletExtensions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace Client.Shared
{
    public partial class MainLayout
    {

        [Inject] protected IDispatcher? dispatcher { get; set; }
        [Inject] protected IJSRuntime? _javascriptRuntime { get; set; }
        [Inject] protected ILocalStorageService? _localStorage { get; set; }
        [Inject] protected IState<WalletState>? walletState { get; set; }
        [Inject] protected IState<ConectedState>? walletConectedState { get; set; }
   
        
        [Inject] protected DialogService? _dialogService { get; set; }

        private bool sidebarExpanded = false;
        private bool SidebarVisible = false;
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                sidebarExpanded = false;              
            }
            base.OnAfterRender(firstRender);
        }
        public async override Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
        }
        private void ToggleSideBar()
        {
            sidebarExpanded = !sidebarExpanded;
            SidebarVisible = true;
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new WalletInitializerAction(_javascriptRuntime, _localStorage, _dialogService));
        }
       
        public async Task OpenWalletConnectors()
        {
            dispatcher.Dispatch(new WalletInitializerAction(_javascriptRuntime, _localStorage, _dialogService));
            var dialogResult = await _dialogService.OpenAsync("Connectar Biletera", RenderWalletConnector);

        }

        public async Task OpenWalletDisconector()
        {

            var dialogResult = await _dialogService.OpenAsync("Desconectar Biletera", RenderWalletDisConnector);

        }
        private RenderFragment RenderWalletConnector(DialogService service)
        {

            RenderFragment fragment = builder =>
            {
                builder.OpenComponent(0, typeof(WalletConnectorComponent));
                builder.CloseComponent();
            };

            return fragment;
        }

        private RenderFragment RenderWalletDisConnector(DialogService service)
        {

            RenderFragment fragment = builder => {
                builder.OpenComponent(0, typeof(WalletDisConnectorComponent));
                builder.CloseComponent();
            };

            return fragment;
        }
    }

}
