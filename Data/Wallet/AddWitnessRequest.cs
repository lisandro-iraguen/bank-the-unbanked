using CardanoSharp.Wallet.Models.Transactions;

namespace Data.Wallet
{
    public class AddWitnessRequest
    {
        public string? WitnessCbor { get; set; }

        public Transaction TxCbor { get; set; }
    }
}
