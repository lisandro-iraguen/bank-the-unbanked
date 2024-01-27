using Components;
using Data.Wallet;
using Microsoft.JSInterop;

namespace Client.State.WalletConnector
{
    public class WalletInitializerResultAction
    {
        public WalletConnectorJsInterop JSInterop { get; }
        public IEnumerable<WalletExtensionState> Extensions { get; }
        public string Key { get; }

        private WalletInitializerResultAction() { }
        public WalletInitializerResultAction(WalletConnectorJsInterop jsInterop, IEnumerable<WalletExtensionState> extensions)
        {

            JSInterop = jsInterop;
            Extensions = extensions;            
        }
    }
}
