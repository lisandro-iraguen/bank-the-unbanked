
using System.Threading.Tasks;

namespace Api.Services.Transaction
{
    public interface ITransactionService
    {
        Task<CardanoSharp.Wallet.Models.Transactions.Transaction> BuildTransaction(string fromAddress, string toAddress, int value);
        Task<CardanoSharp.Wallet.Models.Transactions.Transaction> SignTransaction(string transactionCbor, string witness);

    }
}
