using Data.Oracle;

namespace Client.State.WalletConnecting
{
    public class ChangeConnectingStateAction
    {
        public bool IsConnecting { get; }

        public ChangeConnectingStateAction(bool isConnecting)
        {
            IsConnecting = isConnecting;
        }
    }
}