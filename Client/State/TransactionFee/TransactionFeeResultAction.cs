using Data.Wallet;

namespace Client.State.TransactionFee
{
    public class TransactionFeeResultAction
    {

        public float Fee { get; internal set; }

        public TransactionFeeResultAction() { }
        public TransactionFeeResultAction(float fee)
        {
            Fee = fee;
        }
    }
}