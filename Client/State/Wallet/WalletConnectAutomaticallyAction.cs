using Blazored.LocalStorage;
using Components;
using Data.Wallet;
using Fluxor;
using Microsoft.JSInterop;
using Radzen;

namespace Client.State.Wallet
{
    public class WalletConnectAutomaticallyAction
    {   
        public IEnumerable<WalletExtensionState>? Wallets { get; }
      
      

        private WalletConnectAutomaticallyAction() { }
        public WalletConnectAutomaticallyAction(IEnumerable<WalletExtensionState> wallets)
        {            
            Wallets = wallets;          
        }
    }
}
