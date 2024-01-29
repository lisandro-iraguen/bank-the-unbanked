using CardanoSharp.Wallet.Enums;

namespace Utils
{
    

    public static class ComponentUtils
    {
        public static string ConnectedWalletKey => "connectedWalletKey";
        public static NetworkType GetNetworkType(int networkId)
        {
            switch (networkId)
            {
                case 0: return NetworkType.Testnet;
                case 1: return NetworkType.Mainnet;
                default: return NetworkType.Unknown;
            }
        }


    }

}
