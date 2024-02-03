using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.Services.Transaction;
using System;
using Newtonsoft.Json;
using System.IO;

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
            string walletFrom = req.Query["walletFrom"];
            string walletTo = req.Query["walletto"];
            string value = req.Query["value"];
            if(string.IsNullOrEmpty(walletFrom)) return new BadRequestObjectResult("no wallet address from");
            if(string.IsNullOrEmpty(walletTo)) return new BadRequestObjectResult("no wallet address walletTo");
            if(string.IsNullOrEmpty(value)) return new BadRequestObjectResult("no wallet value");

            
            


            try
            {
                ulong transferValue = ulong.Parse(value);
                var transact = await _transaction.BuildTransaction(walletFrom,walletTo, transferValue);
                return new OkObjectResult(transact);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }


        
    }
}
