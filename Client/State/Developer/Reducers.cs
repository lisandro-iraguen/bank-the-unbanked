using Data.Web;
using Fluxor;

namespace Client.State.Developer
{
    public class Reducers
    {
        [ReducerMethod]
        public static CryptoState ReduceFetchDeveloperAction(CryptoState state, FetchDeveloperAction action) =>
    new(isLoading: true,devs: null);

        [ReducerMethod]
        public static CryptoState ReduceFetchDeveloperResultAction(CryptoState state, FetchDeveloperResultAction action) =>
          new(isLoading: false, devs: action.developers);
    }
}
