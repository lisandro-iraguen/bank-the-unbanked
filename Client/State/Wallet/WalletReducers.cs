using Fluxor;

namespace Client.State.Wallet
{
    public class WalletReducers
    {
        [ReducerMethod]
        public static WalletExtensions ReduceWalletConnectorAction(WalletExtensions state, WalletConnectorAction action) =>
        new(key: action.Key, wallet: null, wallets: action.Wallets);

        [ReducerMethod]
        public static WalletExtensions ReduceWalletConnectorResultAction(WalletExtensions state, WalletConnectorResultAction action) =>
         new(key: state.Key, wallet: action.Wallet, wallets: state.Wallets);

        [ReducerMethod]
        public static WalletExtensions ReduceWalletDisconectAction(WalletExtensions state, WalletDisconectResultAction action)
        {           
            return new(key: null, wallet: null, wallets: null);
        }

        [ReducerMethod]
        public static WalletExtensions ReduceWalletConnectAutomaticallyAction(WalletExtensions state, WalletConnectAutomaticallyAction action)
        {           
            return new(key: null, wallet: null, wallets: state.Wallets);
        }


    }
}
