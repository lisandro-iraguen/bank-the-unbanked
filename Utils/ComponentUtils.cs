using CardanoSharp.Wallet.Enums;

namespace Utils
{
    public static class ComponentUtils
    {
        public static NetworkType GetNetworkType(int networkId)
        {
            switch (networkId)
            {
                case 0: return NetworkType.Testnet;
                case 1: return NetworkType.Mainnet;
                default: return NetworkType.Unknown;
            }
        }

        public static string HexToString(string hexString)
        {
            if (hexString.Length % 2 != 0)
                throw new ArgumentException("Invalid length of hexadecimal string.");

            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }

}
