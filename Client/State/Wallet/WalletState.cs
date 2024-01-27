using Data.Wallet;
using Fluxor;

namespace Client.State.Wallet
{
    [FeatureState]
    public class WalletState
    {
        public bool IsConnecting { get; }
        public IEnumerable<WalletExtensionState> Wallets { get; }

        public string Key { get; }
        public WalletExtensionState Wallet { get; }
        private WalletState() { }

        public WalletState(bool isConnecting,string key, WalletExtensionState wallet, IEnumerable<WalletExtensionState> wallets)
        {
            IsConnecting = isConnecting;
            Key = key;
            Wallet = wallet;
            Wallets = wallets ?? Array.Empty<WalletExtensionState>();
        }

    }
}
