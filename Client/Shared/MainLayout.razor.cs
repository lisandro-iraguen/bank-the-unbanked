using Radzen;
using Microsoft.AspNetCore.Components;
using Data.Wallet;
using Microsoft.JSInterop;
using Blazored.LocalStorage;




namespace Client.Shared
{
    public partial class MainLayout : LayoutComponentBase
    {
        [Inject]
        protected DialogService _dialogService { get; set; }
        [Inject]
        protected HttpClient http { get; set; }
        [Inject]
        protected IJSRuntime? _javascriptRuntime { get; set; }

        [Inject]
        protected ILocalStorageService _localStorage { get; set; }

        private ActionWrapper actionWrapper = new ActionWrapper();
        private WalletConnector _walletConnector;

        private bool isConecting = true;
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

        protected override async Task OnInitializedAsync()
        {
            _dialogService.OnClose += Close;
            actionWrapper.Action = LoadWallet;
            isConecting = true;
            try
            {
                _walletConnector = new WalletConnector(_localStorage, http, _javascriptRuntime);
                await _walletConnector.IntializedWalletAsync();
                WalletSingleton.Instance._walletConnector = _walletConnector;

            }
            catch (Exception ex)
            {
              Console.WriteLine($"Wallet Connection error", ex.Message);
            }
            await _walletConnector.ConnectWalletAutomatically();
            isConecting = false;
            actionWrapper.Action.Invoke();
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
            actionWrapper.Action?.Invoke();

        }

        public void LoadWallet()
        {
            if (WalletSingleton.Instance != null && WalletSingleton.Instance.walletInstance != null)
            {
                Console.WriteLine($"Wallet Loaded {WalletSingleton.Instance.walletInstance.Name}");
            }
            else
            {
                Console.WriteLine($"load wallet not set");                
                Console.WriteLine($"check if the wallet is connected");                
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
