using Fluxor;

namespace Client.State.WalletConnecting;

[FeatureState]
public class WalletConnectingState
{
    public bool IsConnecting { get; }

    public WalletConnectingState() { }
    public WalletConnectingState(bool isConnecting)
    {
        IsConnecting = isConnecting;
    }
}