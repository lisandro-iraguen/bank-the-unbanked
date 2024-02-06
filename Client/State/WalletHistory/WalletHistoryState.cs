using CardanoSharp.Koios.Client.Contracts;
using Data.Wallet;
using Fluxor;

namespace Client.State.WalletHistory
{
    [FeatureState]
    public class WalletHistoryState
    { 
        public AddressTransaction[]? Transactions { get; }
        public bool IsLoading { get; }
        private WalletHistoryState() { }

        public WalletHistoryState(bool isLoading, AddressTransaction[]? transactions)
        {
            IsLoading = isLoading;
            Transactions = transactions;
        }

    }
}
