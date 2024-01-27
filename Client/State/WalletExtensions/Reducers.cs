using Client.State.WalletExtensions;
using Data.Wallet;
using Fluxor;

namespace Client.State.WalletExtensions
{
    public static class Reducers
    {
        [ReducerMethod]
        public static WalletExtensionsState ReduceWalletInitializerAction(WalletExtensionsState state, WalletInitializerAction action) =>
        new(isConnecting: true, js: null, ext: null);

        [ReducerMethod]
        public static WalletExtensionsState ReduceWalletInitializerResultAction(WalletExtensionsState state, WalletInitializerResultAction action) =>
         new(isConnecting: false, js: action.JSInterop, ext: action.Extensions);

       


    }
}
