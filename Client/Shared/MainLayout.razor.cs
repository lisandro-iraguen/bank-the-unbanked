using Radzen;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;
using Data.Wallet;




namespace Client.Shared
{
    public partial class MainLayout: LayoutComponentBase
    {
        [Inject]
        protected DialogService _dialogService { get; set; }

        private bool sidebar1Expanded = false;
        private ActionWrapper actionWrapper = new ActionWrapper();

        protected override void OnInitialized()
        {
            _dialogService.OnClose += Close;
            actionWrapper.Action = LoadWallet;
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
            actionWrapper.Action?.Invoke();
        }

        public void LoadWallet()
        {
            if (WalletSingleton.Instance is not null)
            {
                Console.WriteLine($"Wallet Loaded {WalletSingleton.Instance.Name}");
            }
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
         
            RenderFragment fragment = builder =>
            {
                builder.OpenComponent(0, typeof(WalletDisConnectorComponent));
                builder.CloseComponent();
            };

            return fragment;
        }
    }

}
