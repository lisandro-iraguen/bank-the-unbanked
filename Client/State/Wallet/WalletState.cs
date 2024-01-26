using Components;
using Data.Wallet;
using Fluxor;

namespace Client.State.Wallet.Extension
{
    [FeatureState]

    public class WalletState
    {
        public bool IsLoading { get; }
    
        public WalletConnectorJsInterop WalletConnectorJSInterop { get; }
        public IEnumerable<WalletExtensionState> Extensions { get; }

        private WalletState() { }
        public WalletState(bool isLoading, WalletConnectorJsInterop js, IEnumerable<WalletExtensionState> ext)
        {
            IsLoading = isLoading;
            WalletConnectorJSInterop =js;
            Extensions = ext ?? Array.Empty<WalletExtensionState>();
        }
    }

   
}
