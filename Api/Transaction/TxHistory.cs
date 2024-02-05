using Api.Services.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Threading.Tasks;

namespace Api.Transaction
{
    public class TxHistory
    {
        private readonly ITransactionService _transaction;

        public TxHistory(ITransactionService w)
        {
            _transaction = w;
        }

        [FunctionName("TxHistory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "TxHistory")] HttpRequest req)
        {
            string walletFrom = req.Query["walletFrom"];
            if(string.IsNullOrEmpty(walletFrom)) return new BadRequestObjectResult("no wallet address from");
            
            
            try
            {
                var txHistory = await _transaction.TransactionHistory(walletFrom);
                return new OkObjectResult(txHistory);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }


        
    }
}
