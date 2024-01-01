using Client.Shared;
using Data.Wallet;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Client.Pages
{
    public partial class Home: ComponentBase
    {
        [CascadingParameter]
        private ActionWrapper _actionCommingFromTheMainLayout { get; set; }

        private string walletMessage = null;
        private string symbol = null;
        private string symbolArs = null;
        private string balanceAda = null;
        private string balanceArs = null;
        private string networkType = null;

        private WalletExtensionState walletState=null;
        protected override void OnInitialized()
        {

            _actionCommingFromTheMainLayout.Action += LoadWalletParameters;
        }
        public void LoadWalletParameters()
        {
            walletState = WalletSingleton.Instance;

            walletMessage = $"Wallet Cargada Exitosamente: {walletState.Name} ";
            symbol = walletState.CoinCurrency;
            symbolArs = "$ARS";
            balanceAda = walletState.BalanceAda;
            balanceArs = "0";
            networkType = walletState.Network.ToString();
        }
    }
}
