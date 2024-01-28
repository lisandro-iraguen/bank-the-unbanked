﻿using Fluxor;

namespace Client.State.Wallet
{
    public class WalletReducers
    {
        [ReducerMethod]
        public static WalletState ReduceWalletConnectorAction(WalletState state, WalletConnectorAction action) =>
        new(isConnecting: true, key: action.Key, wallet: null, wallets: action.Wallets);

        [ReducerMethod]
        public static WalletState ReduceWalletConnectorResultAction(WalletState state, WalletConnectorResultAction action) =>
         new(isConnecting: false, key: state.Key, wallet: action.Wallet, wallets: state.Wallets);

        [ReducerMethod]
        public static WalletState ReduceWalletDisconectAction(WalletState state, WalletDisconectAction action)
        {
            _ = state.Wallet.WalletConnectorJs.DisposeAsync();
            return new(isConnecting: false, key: state.Key, wallet: null, wallets: state.Wallets);
        }

        [ReducerMethod]
        public static WalletState ReduceWalletConnectAutomaticallyAction(WalletState state, WalletConnectAutomaticallyAction action)
        {           
            return new(isConnecting: true, key: null, wallet: null, wallets: state.Wallets);
        }


    }
}
