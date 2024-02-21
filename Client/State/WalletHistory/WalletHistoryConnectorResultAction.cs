using CardanoSharp.Koios.Client.Contracts;
using Data.History;
using Data.Wallet;

namespace Client.State.WalletHistory
{
    public class WalletHistoryConnectorResultAction
    {
        public TxHistory[]? Transactions { get; }

        public WalletHistoryConnectorResultAction() { }
        public WalletHistoryConnectorResultAction(TxHistory[] transactions) {
            Transactions = transactions;
        }
    }
}
