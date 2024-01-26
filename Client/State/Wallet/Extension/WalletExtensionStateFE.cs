using Components;
using Data.Wallet;
using Fluxor;

namespace Client.State.Wallet.Extension
{
    [FeatureState]

    public class WalletExtensionStateFE
    {
        public bool IsLoading { get; }
        public IEnumerable<WalletExtensionState> extensions { get; }
        public WalletConnectorJsInterop WalletConnectorJSInterop { get; }
      
        private WalletExtensionStateFE() { }
        public WalletExtensionStateFE(bool isLoading, IEnumerable<WalletExtensionState> ext)
        {
            IsLoading = isLoading;
            extensions = ext ?? Array.Empty<WalletExtensionState>();
        }
    }

   
}
