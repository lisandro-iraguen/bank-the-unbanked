
using Data.Wallet;
using Fluxor;

namespace Client.State.TransactionFee;

[FeatureState]
public class TransactionFeeState
{

    public bool IsLoading { get; }
    public float Fee { get; internal set; }
    public TransactionFeeState() { }
    public TransactionFeeState(bool isLoading, float fee)
    {
        IsLoading = isLoading;
        Fee = fee;
    }
}