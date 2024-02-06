using Fluxor;

namespace Client.State.WalletHistory
{
    public class WalletHistoryReducer
    {
        [ReducerMethod]
        public static WalletHistoryState ReduceWalletHistoryAction(WalletHistoryState state, WalletHistoryConnectorAction action) =>
        new(isLoading: true,transactions:null);

        [ReducerMethod]
        public static WalletHistoryState ReduceWalletHistoryResultAction(WalletHistoryState state, WalletHistoryConnectorResultAction action) =>
         new(isLoading: false, transactions: action.Transactions);



    }
}
