using Radzen;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;




namespace Client.Shared
{
    public partial class MainLayout: LayoutComponentBase
    {
        [Inject]
        protected DialogService _dialogService { get; set; }

        private bool sidebar1Expanded = false;

        protected override void OnInitialized()
        {
            _dialogService.OnClose += Close;
        }
        public async Task OpenWalletConnectors()
        {

            var dialogResult = await _dialogService.OpenAsync("Connectar Biletera", RenderDialogContent);
            
        }

        private void Close(dynamic obj)
        {
            StateHasChanged();
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
