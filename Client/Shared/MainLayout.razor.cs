﻿using Blazored.LocalStorage;
using Client.State.Wallet;
using Client.State.WalletConnecting;
using Client.State.WalletExtensions;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;




namespace Client.Shared
{
    public partial class MainLayout
    {

        [Inject] protected IDispatcher dispatcher { get; set; }
        [Inject] protected IJSRuntime? _javascriptRuntime { get; set; }
        [Inject] protected ILocalStorageService _localStorage { get; set; }
        [Inject] protected IState<WalletExtensionsState> walletConectorState { get; set; }
        [Inject] protected IState<WalletState> walletState { get; set; }
        //[Inject] protected ILocalStorageService _localStorage { get; set; }
        [Inject] protected DialogService _dialogService { get; set; }

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
