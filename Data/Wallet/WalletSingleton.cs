using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Wallet
{
    public class WalletSingleton
    {
        private static WalletExtensionState instance = null;
        private static readonly object padlock = new object();
        public string walletId;
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
