using Client.State.Transaction;
using Fluxor;

namespace Client.State.TransactionFee
{
    public static class ReducersFee
    {
        [ReducerMethod]
        public static TransactionFeeState ReduceTransactionFeeAction(TransactionFeeState state, TransactionFeeAction action) =>
            new(isLoading:true, fee:0);
        
        [ReducerMethod]
        public static TransactionFeeState ReduceTransactionFeeResetAction(TransactionFeeState state, TransactionFeeResetAction action) =>
            new(isLoading:false, fee:0);


        [ReducerMethod]
        public static TransactionFeeState ReduceTransactionFeeResultAction(TransactionFeeState state, TransactionFeeResultAction action) =>
         new(isLoading: false, fee:action.Fee); 
        

       
    }
}