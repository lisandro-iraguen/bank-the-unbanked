
using Client.State.Wallet;
using Client.State.WalletConnecting;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Shared
{
    public partial class WalletConnect 
    {
        [Inject] protected IDispatcher dispatcher { get; set; }

        [Inject] protected IState<WalletState> walletState { get; set; }
        [Inject] protected IState<WalletConnectingState> walletConectingState { get; set; }
        [Inject] protected DialogService _dialogService { get; set; }
     


        protected override void OnInitialized()
        {
            _dialogService.OnClose += Close;
            _dialogService.OnRefresh += OnRefresh;

        }

        private void OnRefresh()
        {
            StateHasChanged();
        }

        public async Task OpenWalletConnectors()
        {
            var dialogResult = await _dialogService.OpenAsync("Connectar Biletera", RenderWalletConnector);

        }

        public async Task OpenWalletDisconector()
        {

            var dialogResult = await _dialogService.OpenAsync("Desconectar Biletera", RenderWalletDisConnector);

        }

        private void Close(dynamic obj)
        {
            StateHasChanged();

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

            RenderFragment fragment = builder =>            {
                builder.OpenComponent(0, typeof(WalletDisConnectorComponent));
                builder.CloseComponent();
            };

            return fragment;
        }
    }
}
