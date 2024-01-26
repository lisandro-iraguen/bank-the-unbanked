using Fluxor;

namespace Client.State.Wallet.Extension
{
    public class Reducers
    {
        [ReducerMethod]
        public static WalletExtensionStateFE ReduceFetchExtensionAction(WalletExtensionStateFE state, FetchExtensionAction action) =>
    new(isLoading: true,ext: null);

        [ReducerMethod]
        public static WalletExtensionStateFE ReduceFetchExtensionResultAction(WalletExtensionStateFE state, FetchExtensionResultAction action) =>
          new(isLoading: false, ext: action.extensions);
    }
}
