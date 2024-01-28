
using Data.Wallet;
using Fluxor;

namespace Client.State.Transaction;

[FeatureState]
public class TransactionState
{
    public bool IsSigningTransaction { get; }
    public WalletExtensionState UsedWallet { get; }

    public TransactionState() { }
    public TransactionState(bool isSigningTransaction, WalletExtensionState wallet)
    {
        UsedWallet = wallet;
        IsSigningTransaction = isSigningTransaction;
    }
}