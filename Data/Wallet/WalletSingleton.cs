using Utils.Components;

namespace Data.Wallet
{
    public class WalletSingleton
    {
        private static WalletExtensionState instance = null;
        private static readonly object padlock = new object();
        public string walletId;
        public static WalletConnectorJsInterop walletConnectorJs = null;
        public static WalletExtensionState Instance
        {
            get
            {
                
                    return instance;
                
            }
            set { 
                instance = value;
            }
        }
    }
}
