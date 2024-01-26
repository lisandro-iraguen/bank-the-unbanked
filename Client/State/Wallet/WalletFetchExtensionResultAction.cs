using Data.Wallet;

namespace Client.State.Wallet.Extension
{
    public class WalletFetchExtensionResultAction
    {

        public IEnumerable<WalletExtensionState> extensions { get; }

        public WalletFetchExtensionResultAction(IEnumerable<WalletExtensionState> ext)
        {
            extensions = ext;
        }

    }
}
