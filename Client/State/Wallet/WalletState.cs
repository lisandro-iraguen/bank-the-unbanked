using Data.Wallet;
using Fluxor;

namespace Client.State.Wallet
{
    [FeatureState]
    public class WalletExtensions
    {
       
        public IEnumerable<WalletExtensionState>? Wallets { get; }
        public string? Key { get; }
        public WalletExtensionState? Wallet { get; }
        private WalletExtensions() { }

        public WalletExtensions(string key, WalletExtensionState wallet, IEnumerable<WalletExtensionState> wallets)
        {         
            Key = key;
            Wallet = wallet;
            Wallets = wallets ?? Array.Empty<WalletExtensionState>();
        }

    }
}
