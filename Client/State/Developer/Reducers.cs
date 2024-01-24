using Data.Web;
using Fluxor;

namespace Client.State.Developer
{
    public class Reducers
    {
        [ReducerMethod]
        public static DeveloperState ReduceFetchDeveloperAction(DeveloperState state, FetchDeveloperAction action) =>
    new(isLoading: true,devs: null);

        [ReducerMethod]
        public static DeveloperState ReduceFetchDeveloperResultAction(DeveloperState state, FetchDeveloperResultAction action) =>
          new(isLoading: false, devs: action.developers);
    }
}
