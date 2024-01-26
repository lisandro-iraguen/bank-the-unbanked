using Client.State.Wallet.Extension;
using Data.Wallet;
using Fluxor;

namespace Client.State.Wallet
{
    public class Reducers
    {
        [ReducerMethod]
        public static WalletState ReduceWalletConnectAction(WalletState state, WalletConnectAction action) =>
    new(isLoading: true,js: null,ext: null);

        [ReducerMethod]
        public static WalletState ReduceFetchExtensionAction(WalletState state, WalletFtechExtensionsAction action) =>
        new(isLoading: true, js: action.WalletConnectorJSInterop, ext: null);
        [ReducerMethod]
        public static WalletState ReduceFetchExtensionResultAction(WalletState state, WalletFetchExtensionResultAction action) =>
          new(isLoading: false, js: null, ext: action.extensions);
    }
}
