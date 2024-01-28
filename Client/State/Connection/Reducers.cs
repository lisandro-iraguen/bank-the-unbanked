
using Fluxor;

namespace Client.State.Connection
{
    public static class Reducers
    {
        [ReducerMethod]
        public static ConectedState ReduceIncrementCounterAction(ConectedState state, IsConnectedConectionAction action) =>
            new(isConnecting: true); 
        
        [ReducerMethod]
        public static ConectedState ReduceIncrementCounterAction(ConectedState state, IsNotConnectedConectionAction action) =>
            new(isConnecting: false);
    }
}