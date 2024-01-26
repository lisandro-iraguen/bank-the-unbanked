using Components;
using Microsoft.JSInterop;

namespace Client.State.Wallet
{
    public class WalletFtechExtensionsAction
    {
        public WalletConnectorJsInterop WalletConnectorJSInterop { get; }
        public IJSRuntime JsRuntime { get; }

        private WalletFtechExtensionsAction() { }
        public WalletFtechExtensionsAction(WalletConnectorJsInterop js, IJSRuntime jSRuntime)
        {
            WalletConnectorJSInterop = js;
            JsRuntime = jSRuntime;
        }
    }
}
