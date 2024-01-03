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

        private bool sidebar1Expanded = false;
        private ActionWrapper actionWrapper = new ActionWrapper();
        private WalletConnector _walletConnector;

        private bool isConecting = true;

        protected override async Task OnInitializedAsync()
        {
            _dialogService.OnClose += Close;
            actionWrapper.Action = LoadWallet;
            isConecting = true;
            try
            {
                _walletConnector = new WalletConnector(_localStorage, http, _javascriptRuntime);
                await _walletConnector.IntializedWalletAsync();
                WalletSingleton.Instance._walletConnector= _walletConnector;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await _walletConnector.ConnectWallet();
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
            if (WalletSingleton.Instance.walletInstance is not null)
            {
                Console.WriteLine($"Wallet Loaded {WalletSingleton.Instance.walletInstance.Name}");
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
