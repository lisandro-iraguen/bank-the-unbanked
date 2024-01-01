using Client.Shared;
using Data.Wallet;
using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Utils;

namespace Client.Pages
{
    public partial class Home: ComponentBase
    {
        [Inject]
        protected IConfiguration _configuration { get; set; }

        [CascadingParameter]
        private ActionWrapper _actionCommingFromTheMainLayout { get; set; }

        private string walletMessage = null;
        private string symbol = null;
        private string symbolArs = null;
        private string balanceAda = null;
        private string balanceArs = null;
        private string AssetsID=null;
        private string PolicyAssetsID=null;
        private string networkType = null;

        private WalletExtensionState walletState=null;
        protected override void OnInitialized()
        {

            _actionCommingFromTheMainLayout.Action += LoadWalletParameters;
            AssetsID = _configuration.GetValue<string>("AppSettings:AssetId");
            PolicyAssetsID = _configuration.GetValue<string>("AppSettings:PolicyAssetId");

            if(AssetsID is null)
            {
                throw new Exception("AssetID cannot be null");
            }
        }
        
        public void LoadWalletParameters()
        {
            walletState = WalletSingleton.Instance;

            ulong nativeAmount= walletState.NativeAssets[PolicyAssetsID+"-"+AssetsID];


            walletMessage = $"Wallet Cargada Exitosamente: {walletState.Name} ";
            symbol = walletState.CoinCurrency;
            symbolArs = ComponentUtils.HexToString(AssetsID);
            balanceAda = walletState.BalanceAda;
            balanceArs = nativeAmount.ToString();
            networkType = walletState.Network.ToString();
        }
    }
}
