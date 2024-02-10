using Data.Wallet;

namespace Api.Services.Wallet
{
    public interface IWalletData
    {
        Task<IEnumerable<WalletExtension>> GetWalletData();

    }
}
