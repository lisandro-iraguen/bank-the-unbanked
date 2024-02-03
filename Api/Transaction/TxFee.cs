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
    public class TxFee
    {
        private readonly ITransactionService _transaction;

        public TxFee(ITransactionService w)
        {
            _transaction = w;
        }

        [FunctionName("TxFee")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "TxFee")] HttpRequest req)
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
                var fee = await _transaction.CalculateFee(walletFrom,walletTo, transferValue);
                return new OkObjectResult(fee);
            }
            catch(Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }


        
    }
}
