using Data.Wallet;

namespace Client.State.Transaction
{
    public class SignTransactionAction
    {
        public WalletExtensionState? UsedWallet { get; }
        public string? TransferTo { get; internal set; }
        public float TransferAmount { get; internal set; }

        public SignTransactionAction() { }
        public SignTransactionAction(WalletExtensionState wallet,string transferTo, float ammount)
        {
            UsedWallet = wallet;
            TransferTo = transferTo;
            TransferAmount = ammount;
        }
    }
}