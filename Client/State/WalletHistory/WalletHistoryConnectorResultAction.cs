using CardanoSharp.Koios.Client.Contracts;
using Data.Wallet;

namespace Client.State.WalletHistory
{
    public class WalletHistoryConnectorResultAction
    {
        public AddressTransaction[]? Transactions { get; }

        public WalletHistoryConnectorResultAction() { }
        public WalletHistoryConnectorResultAction(AddressTransaction[] transactions) {
            Transactions = transactions;
        }
    }
}
