using Data.Wallet;

namespace Client.State.Transaction
{
    public class SignTransactionResultAction
    {
        public WalletExtensionState UsedWallet { get; }

        public SignTransactionResultAction() { }
        public SignTransactionResultAction(WalletExtensionState wallet)
        {
            UsedWallet = wallet;
        }
    }
}