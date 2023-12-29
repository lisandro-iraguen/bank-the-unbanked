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

            var dialogResult = await _dialogService.OpenAsync("Connectar Biletera", RenderDialogContent);
            
        }

        private void Close(dynamic obj)
        {
            StateHasChanged();
            actionWrapper.Action?.Invoke();
        }

        public void LoadWallet()
        {
            Console.WriteLine($"Wallet Loaded {WalletSingleton.Instance.Name}");
        }

        private RenderFragment RenderDialogContent(DialogService service)
        {
         
            RenderFragment fragment = builder =>
            {
                builder.OpenComponent(0, typeof(WalletConnectorComponent));
                builder.CloseComponent();
            };

            return fragment;
        }
    }

}
