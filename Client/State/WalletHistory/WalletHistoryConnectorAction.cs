using Data.Wallet;

namespace Client.State.WalletHistory
{
    public class WalletHistoryConnectorAction
    {
     
        public WalletExtensionState? Wallet { get; }        
       

        public WalletHistoryConnectorAction(WalletExtensionState wallet)
        {
            Wallet = wallet;
        }
    }
}
