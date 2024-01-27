
using Fluxor;

namespace Client.State.WalletConnecting
{
    public static class Reducers
    {
        [ReducerMethod]
        public static WalletConnectingState ReduceChangeConnectingStateAction(WalletConnectingState state, ChangeConnectingStateAction action) =>
            new(isConnecting: action.IsConnecting);
    }
}