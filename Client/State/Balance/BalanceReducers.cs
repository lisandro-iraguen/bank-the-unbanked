
using Fluxor;

namespace Client.State.Balance
{
    public static class BalanceReducers
    {
        [ReducerMethod]
        public static BalanceState ReduceUpdateBalanceAction(BalanceState state, IsUpdateingWalletBalance action) =>
            new(isUpdating: true); 
        
        [ReducerMethod]
        public static BalanceState ReduceStopUpdateBalanceAction(BalanceState state, IsNotUpdateingWalletBalance action) =>
            new(isUpdating: false);
    }
}