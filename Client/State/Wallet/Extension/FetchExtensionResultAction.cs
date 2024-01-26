using Data.Wallet;

namespace Client.State.Wallet.Extension
{
    public class FetchExtensionResultAction
    {

        public IEnumerable<WalletExtensionState> extensions { get; }

        public FetchExtensionResultAction(IEnumerable<WalletExtensionState> ext)
        {
            extensions = ext;
        }

    }
}
