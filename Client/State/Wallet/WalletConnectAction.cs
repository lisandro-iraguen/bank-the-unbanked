using Components;
using Microsoft.JSInterop;

namespace Client.State.Wallet
{
    public class WalletConnectAction
    {        
        public IJSRuntime JsRuntime { get; }

        private WalletConnectAction() { }
        public WalletConnectAction(IJSRuntime jSRuntime)
        {
            
            JsRuntime = jSRuntime;
        }
    }
}
