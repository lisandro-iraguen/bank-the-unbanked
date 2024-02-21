
using CardanoSharp.Koios.Client.Contracts;
using Data.History;
using System.Threading.Tasks;

namespace Api.Services.Transaction
{
    public interface ITransactionService
    {
        Task<CardanoSharp.Wallet.Models.Transactions.Transaction> BuildTransaction(string fromAddress, string toAddress, ulong value);
        Task<CardanoSharp.Wallet.Models.Transactions.Transaction> SignTransaction(string transactionCbor, string witness);
        Task<ulong> CalculateFee(string fromAddress, string toAddress, ulong value);
        Task<TxHistory[]> TransactionHistory(string addressFrom, string caranoScanUrl);
    }
}
