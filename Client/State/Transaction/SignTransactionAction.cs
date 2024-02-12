using Data.Wallet;

namespace Client.State.Transaction
{
    public class SignTransactionAction
    {
        public WalletExtensionState? UsedWallet { get; }
        public string? TransferTo { get; internal set; }
        public string? TransferInfoSuccess { get; internal set; }
        public string? TransferSuccessMessage { get; internal set; }
        public float TransferAmount { get; internal set; }


        public SignTransactionAction() { }
        public SignTransactionAction(WalletExtensionState wallet,string transferTo, float ammount,string transferInfoSuccess,string transferSuccessMessage)
        {
            UsedWallet = wallet;
            TransferTo = transferTo;
            TransferAmount = ammount;
            TransferInfoSuccess = transferInfoSuccess;
            TransferSuccessMessage = transferSuccessMessage;
        }
    }
}