using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.Services.Transaction;
using System;

namespace Api.Transaction
{
    public class TxBuilder
    {
        private readonly ITransactionService _transaction;

        public TxBuilder(ITransactionService w)
        {
            _transaction = w;
        }

        [FunctionName("TxBuilder")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "TxBuild")] HttpRequest req)
        {
            string wallet = req.Query["wallet"];
            string value = req.Query["value"];
            if(string.IsNullOrEmpty(wallet)) return new BadRequestObjectResult("no wallet address");
            if(string.IsNullOrEmpty(value)) return new BadRequestObjectResult("no wallet value");

            try
            {
                int transferValue = int.Parse(value);
                var transact = await _transaction.BuildTransaction(wallet, transferValue);
                return new OkObjectResult(transact);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }


        
    }
}
