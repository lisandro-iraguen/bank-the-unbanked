﻿using Fluxor;

namespace Client.State.Transaction
{
    public static class Reducers
    {
        [ReducerMethod]
        public static TransactionState ReduceSignTransactionAction(TransactionState state, SignTransactionAction action) =>
            new(isSigningTransaction:true, wallet: action.UsedWallet);


        [ReducerMethod]
        public static TransactionState ReduceSignTransactionResultAction(TransactionState state, SignTransactionResultAction action) =>
         new(isSigningTransaction: false, wallet: action.UsedWallet); 
        

        [ReducerMethod]
        public static TransactionState ReduceSignTransactionFailedResultAction(TransactionState state, SignTransactionFailedResultAction action) =>
         new(isSigningTransaction: false, wallet: state.UsedWallet);
    }
}