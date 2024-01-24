using Data.Web;
using Fluxor;

namespace Client.State.Crypto

{
    public class Reducers
    {
        [ReducerMethod]
        public static CryptoState ReduceFetchDeveloperAction(CryptoState state, FetchCryptoAction action) =>
    new(isLoading: true,crypto: null);

        [ReducerMethod]
        public static CryptoState ReduceFetchDeveloperResultAction(CryptoState state, FetchCryptoResultAction action) =>
          new(isLoading: false, crypto: action.criptoDTO);
    }
}
