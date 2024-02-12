using Fluxor;

namespace Client.State.Balance;

[FeatureState]
public class BalanceState
{
    public bool IsUpdating { get; } = false;

    public BalanceState() { }
    public BalanceState(bool isUpdating)
    {
        IsUpdating = isUpdating;
    }
}