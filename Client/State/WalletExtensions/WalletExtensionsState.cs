using Components;
using Data.Wallet;
using Fluxor;

namespace Client.State.WalletExtensions
{
    [FeatureState]

    public class WalletExtensionsState
    {
        public bool IsConnecting { get; }

        public WalletConnectorJsInterop WalletConnectorJSInterop { get; }
        public IEnumerable<WalletExtensionState> Extensions { get; }
      
        private WalletExtensionsState() { }

        public WalletExtensionsState(bool isConnecting, WalletConnectorJsInterop js, IEnumerable<WalletExtensionState> ext)
        {
            IsConnecting = isConnecting;
            WalletConnectorJSInterop = js;
            Extensions = ext ?? Array.Empty<WalletExtensionState>();            
        }


    }


}
