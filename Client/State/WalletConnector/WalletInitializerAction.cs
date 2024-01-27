using Components;
using Microsoft.JSInterop;

namespace Client.State.WalletConnector
{
    public class WalletInitializerAction
    {
        public IJSRuntime JsRuntime { get; }

        private WalletInitializerAction() { }
        public WalletInitializerAction(IJSRuntime jSRuntime)
        {

            JsRuntime = jSRuntime;
        }
    }
}
