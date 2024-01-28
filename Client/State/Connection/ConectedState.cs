using Fluxor;

namespace Client.State.Connection;

[FeatureState]
public class ConectedState
{
    public bool IsConnecting { get; } = false;

    public ConectedState() { }
    public ConectedState(bool isConnecting)
    {
        IsConnecting = isConnecting;
    }
}