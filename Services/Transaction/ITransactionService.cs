
using CardanoSharp.Koios.Client.Contracts;
using System.Threading.Tasks;

namespace Api.Services.Transaction
{
    public interface ITransactionService
    {
        Task<CardanoSharp.Wallet.Models.Transactions.Transaction> BuildTransaction(string fromAddress, string toAddress, ulong value);
        Task<CardanoSharp.Wallet.Models.Transactions.Transaction> SignTransaction(string transactionCbor, string witness);
        Task<ulong> CalculateFee(string fromAddress, string toAddress, ulong value);
        Task<AddressTransaction[]> TransactionHistory(string addressFrom);
    }
}
