using Data.Wallet;
using Fluxor;

namespace Client.State.WalletConnector
{
    public static class Reducers
    {
        [ReducerMethod]
        public static WalletConectorState ReduceWalletInitializerAction(WalletConectorState state, WalletInitializerAction action) =>
        new(isConnecting: true, js: null, ext: null);

        [ReducerMethod]
        public static WalletConectorState ReduceWalletInitializerResultAction(WalletConectorState state, WalletInitializerResultAction action) =>
         new(isConnecting: false, js: action.JSInterop, ext: action.Extensions);

       


    }
}
