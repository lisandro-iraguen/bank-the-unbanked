using CardanoSharp.Wallet.Enums;
using System.Collections.Generic;
using System.Globalization;
using Components;
using System;
using CardanoSharp.Wallet.Models.Addresses;
using System.Text;
using CardanoSharp.Wallet.Extensions;

namespace Data.Wallet
{
    public class WalletExtensionState : WalletExtension
    {
        public string[] UsedAdress { get; set; }
        public WalletConnectorJsInterop WalletConnectorJs { get; set; }
        public bool Installed { get; set; }

        public bool Enabled { get; set; }

        public bool Connected { get; set; }

        public string Version { get; set; }

        public ulong Balance { get; set; } 
        
        public ulong Lovlace {
            get
            {
                return 1000000;
            }
                
        }

        public string BalanceAda
        {
            get
            {
                var temp = (Balance / (ulong)Lovlace).ToString("N", CultureInfo.CreateSpecificCulture("en-US"));
                return temp.Substring(0, temp.IndexOf('.'));
            }
        }

        public decimal BalanceAda2
        {
            get
            {
                return (Balance / (ulong)Lovlace);
            }
        }

        public string BalanceRemainder
        {
            get
            {
                return (Balance % 1000000).ToString("D6");
            }
        }

        public string LastUsedAddress
        {
            get
            {
                var adress= new Address(UsedAdress[0].HexToByteArray());
                return adress.ToString();
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
        public NetworkType Network { get; set; }

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