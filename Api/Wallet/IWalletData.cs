using Data.Wallet;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Wallet
{
    public interface IWalletData
    {
        Task<IEnumerable<WalletExtension>> GetWalletData();

    }
}
