using Fluxor;

namespace Client.State.Wallet
{
    public class WalletReducers
    {
        [ReducerMethod]
        public static WalletState ReduceWalletConnectorAction(WalletState state, WalletConnectorAction action) =>
        new(key: action.Key, wallet: null, wallets: action.Wallets);

        [ReducerMethod]
        public static WalletState ReduceWalletConnectorResultAction(WalletState state, WalletConnectorResultAction action) =>
         new(key: state.Key, wallet: action.Wallet, wallets: state.Wallets);

        [ReducerMethod]
        public static WalletState ReduceWalletDisconectAction(WalletState state, WalletDisconectResultAction action)
        {           
            return new(key: null, wallet: null, wallets: null);
        }

        [ReducerMethod]
        public static WalletState ReduceWalletConnectAutomaticallyAction(WalletState state, WalletConnectAutomaticallyAction action)
        {           
            return new(key: null, wallet: null, wallets: state.Wallets);
        }


    }
}
