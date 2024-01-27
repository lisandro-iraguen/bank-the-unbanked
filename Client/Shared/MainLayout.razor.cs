using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;




namespace Client.Shared
{
    public partial class MainLayout 
    {
      
        //[Inject] protected HttpClient http { get; set; }
        [Inject] protected IJSRuntime? _javascriptRuntime { get; set; }
        //[Inject] protected ILocalStorageService _localStorage { get; set; }
     

        //private ActionWrapper actionWrapper = new ActionWrapper();
        //private WalletConnector _walletConnector;

        //private bool isConecting = true;
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
            //_dialogService.OnClose += Close;
            //actionWrapper.Action = LoadWallet;
          

            //isConecting = true;
            //try
            //{
            //    _walletConnector = new WalletConnector(_localStorage, http, _javascriptRuntime);
            //    await _walletConnector.IntializedWalletAsync();
            //    WalletSingleton.Instance._walletConnector = _walletConnector;

            //}
            //catch (Exception ex)
            //{
            //  Console.WriteLine($"Wallet Connection error", ex.Message);
            //}
            //await _walletConnector.ConnectWalletAutomatically();
            //isConecting = false;
            //actionWrapper.Action.Invoke();
            //StateHasChanged();
        }
       

        private void Close(dynamic obj)
        {
           
            //actionWrapper.Action?.Invoke();
            //this.StateHasChanged();
            //Console.WriteLine(walletState.Value.IsConnecting);
        }

        public void LoadWallet()
        {
            //if (WalletSingleton.Instance != null && WalletSingleton.Instance.walletInstance != null)
            //{
            //    Console.WriteLine($"Wallet Loaded {WalletSingleton.Instance.walletInstance.Name}");
            //}
            //else
            //{
            //    Console.WriteLine($"load wallet not set");                
            //    Console.WriteLine($"check if the wallet is connected");                
            //}
        }

       
    }

}
