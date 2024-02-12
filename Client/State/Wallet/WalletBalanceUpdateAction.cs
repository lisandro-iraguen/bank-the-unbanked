using Data.Wallet;

namespace Client.State.Wallet
{
    public class WalletBalanceUpdateAction
    {
        public WalletExtensionState? Wallet { get; }        

        public WalletBalanceUpdateAction(WalletExtensionState? wallet)
        {
            Wallet = wallet;          
        }

    }
}
