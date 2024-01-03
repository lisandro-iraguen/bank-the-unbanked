using Components;
using System;

namespace Data.Wallet
{
    public class WalletSingleton
    {
        public WalletExtensionState walletInstance { get; set; }
        public string walletId;
        public WalletConnectorJsInterop walletConnectorJs = null;
        public WalletConnector _walletConnector = null;
        private readonly static WalletSingleton _instance = new WalletSingleton();

        private WalletSingleton()
        {
        }

        public static WalletSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

       
    }
}
