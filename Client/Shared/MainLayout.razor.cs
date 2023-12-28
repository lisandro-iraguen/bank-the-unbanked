using Radzen;
using Microsoft.AspNetCore.Components;




namespace Client.Shared
{
    public partial class MainLayout: LayoutComponentBase
    {
        [Inject]
        protected DialogService _dialogService { get; set; }
        private bool sidebar1Expanded = true;

        protected override void OnInitialized()
        {
            
        }

        
        public async Task OpenWalletConnectors()
        {

            var dialogResult = await _dialogService.OpenAsync("Connectar Biletera", RenderDialogContent);
            Console.WriteLine(dialogResult);
            
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
