using Data.Wallet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Utils.Components;

namespace Client.Shared
{
    public partial class WalletDisConnectorComponent
    {
        [Inject]
        protected DialogService _dialogService { get; set; }

        [Inject]
        protected IJSRuntime _javascriptRuntime { get; set; }

     
        public WalletExtensionState? _connectedWallet { get; private set; }
        private string usedWallet = "";


        protected override async Task OnInitializedAsync()
        {
            _connectedWallet = WalletSingleton.Instance;
            usedWallet = _connectedWallet.UsedAdress.First();
        }



        public async ValueTask DisconnectWalletAsync(bool suppressEvent = false)
        {
            await _connectedWallet.WalletConnectorJs!.DisposeAsync();
            _connectedWallet.Connected = false;
            _connectedWallet = null;
            WalletSingleton.Instance = null;
            StateHasChanged();
            _dialogService.Close();
            return;
        }



    }



}
