using Data.Wallet;

namespace Client.State.TransactionFee
{
    public class TransactionFeeAction
    {
        public WalletExtensionState UsedWallet { get; }
        public string TransferTo { get; internal set; }
        public ulong TransferAmount { get; internal set; }

        public TransactionFeeAction() { }
        public TransactionFeeAction(WalletExtensionState wallet, string transferTo, ulong ammount)
        {
            UsedWallet = wallet;
            TransferTo = transferTo;
            TransferAmount = ammount;
        }
    }
}