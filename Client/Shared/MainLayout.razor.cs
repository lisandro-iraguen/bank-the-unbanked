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
        [Inject] protected IState<WalletExtensions>? walletState { get; set; }
        [Inject] protected IState<ConectedState>? walletConectedState { get; set; }
        [Inject] protected DialogService? _dialogService { get; set; }
        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        private bool sidebarExpanded = false;
        private bool SidebarVisible = false;
        private I18nText.Web? webText;

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

        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);

        }
        public async Task OpenWalletConnectors()
        {
            dispatcher.Dispatch(new WalletInitializerAction(_javascriptRuntime, _localStorage, _dialogService));
            var dialogResult = await _dialogService.OpenAsync(webText.ConnectWallet, RenderWalletConnector);

        }

        public async Task OpenWalletDisconector()
        {

            var dialogResult = await _dialogService.OpenAsync(webText.DisconnectWallet, RenderWalletDisConnector);

        }

        private async Task OnChangeCurrentLang()
        {
            var language = await I18nText.GetCurrentLanguageAsync();

            if (language == "es-419")
            {
                await I18nText.SetCurrentLanguageAsync("en");
            }
            else
            {
                if (language == "en")
                    await I18nText.SetCurrentLanguageAsync("es-419");
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
