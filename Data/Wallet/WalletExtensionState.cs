using System.Collections.Generic;
using System.Globalization;

namespace Data.Wallet
{
    public class WalletExtensionState : WalletExtension
    {
        public bool Installed { get; set; }

        public bool Enabled { get; set; }

        public bool Connected { get; set; }

        public string Version { get; set; }

        public ulong Balance { get; set; }

        public string BalanceAda
        {
            get
            {
                var temp = (Balance / 1000000).ToString("N", CultureInfo.CreateSpecificCulture("en-US"));
                return temp.Substring(0, temp.IndexOf('.'));
            }
        }

        public string BalanceRemainder
        {
            get
            {
                return (Balance % 1000000).ToString("D6");
            }
        }



        public string CoinCurrency
        {
            get
            {
                return "t₳";

            }
        }

        public ulong TokenPreservation { get; set; }

        public string TokenPreservationAda
        {
            get
            {
                var temp = (TokenPreservation / 1000000).ToString("N", CultureInfo.CreateSpecificCulture("en-US"));
                return temp.Substring(0, temp.IndexOf('.'));
            }
        }

        public string TokenPreservationRemainder
        {
            get
            {
                return (TokenPreservation % 1000000).ToString("D6");
            }
        }

        public int TokenCount { get; set; }

        public Dictionary<string, ulong> NativeAssets { get; set; } = new();

        public WalletExtensionState()
        {
        }

        public WalletExtensionState(WalletExtensionState copy)
            : base(copy)
        {
            Installed = copy.Installed;
            Enabled = copy.Enabled;
            Connected = copy.Connected;
            Version = copy.Version;
            Balance = copy.Balance;
            TokenPreservation = copy.TokenPreservation;
            TokenCount = copy.TokenCount;
            NativeAssets = copy.NativeAssets;
        }

        public WalletExtensionState(WalletExtension copy)
                : base(copy)
        {
        }
    }
}