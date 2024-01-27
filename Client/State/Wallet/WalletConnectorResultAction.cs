using Data.Wallet;
using Radzen;

namespace Client.State.Wallet;
public class WalletConnectorResultAction
{
    public WalletExtensionState Wallet { get; }

    private WalletConnectorResultAction() { }
    public WalletConnectorResultAction(WalletExtensionState wallet)
    {
        Wallet = wallet;
    }
}

