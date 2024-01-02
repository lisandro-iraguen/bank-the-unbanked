using Client.Shared;
using Data.Wallet;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Utils;

namespace Client.Pages
{
    public partial class Home : ComponentBase
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
        private string AssetsID = null;
        private string PolicyAssetsID = null;
        private string networkType = null;

        private int valueToTransfer;

        private WalletExtensionState walletState = null;
        protected override void OnInitialized()
        {

            _actionCommingFromTheMainLayout.Action += LoadWalletParameters;
            AssetsID = _configuration.GetValue<string>("AppSettings:AssetId");
            PolicyAssetsID = _configuration.GetValue<string>("AppSettings:PolicyAssetId");

            if (AssetsID is null)
            {
                throw new Exception("AssetID cannot be null");
            }
        }

        public void LoadWalletParameters()
        {
            walletState = WalletSingleton.Instance;
            if (walletState is not null)
            {
                ulong nativeAmount = walletState.NativeAssets[PolicyAssetsID + "-" + AssetsID];

                walletMessage = $"Wallet Cargada Exitosamente: {walletState.Name} ";
                symbol = walletState.CoinCurrency;
                symbolArs = ComponentUtils.HexToString(AssetsID);
                balanceAda = walletState.BalanceAda;
                balanceArs = nativeAmount.ToString();
                networkType = walletState.Network.ToString();
            }
        }


        void OnChangeWalletAdress(string value, string name)
        {
            Console.WriteLine($"{name} value changed to {value}");
        }

        private async Task transfer()
        {
            Console.WriteLine("transfer to anoter account");
        }

    }
}
