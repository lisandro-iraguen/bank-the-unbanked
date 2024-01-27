using Components;
using Data.Wallet;
using Fluxor;

namespace Client.State.WalletConnector
{
    [FeatureState]

    public class WalletConectorState
    {
        public bool IsConnecting { get; }

        public WalletConnectorJsInterop WalletConnectorJSInterop { get; }
        public IEnumerable<WalletExtensionState> Extensions { get; }
      
        private WalletConectorState() { }

        public WalletConectorState(bool isConnecting, WalletConnectorJsInterop js, IEnumerable<WalletExtensionState> ext)
        {
            IsConnecting = isConnecting;
            WalletConnectorJSInterop = js;
            Extensions = ext ?? Array.Empty<WalletExtensionState>();            
        }


    }


}
