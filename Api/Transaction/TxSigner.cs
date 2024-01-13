using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.Services.Transaction;
using System.IO;
using Newtonsoft.Json;
using Data.Wallet;

namespace Api.Transaction
{
    public class TxSigner
    {
        private readonly ITransactionService _transaction;

        public TxSigner(ITransactionService w)
        {
            _transaction = w;
        }

        [FunctionName("TxSigner")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "TxSign")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var requestData = JsonConvert.DeserializeObject<TxRequest>(requestBody);
             var transaction = await _transaction.SignTransaction(requestData.transactionCbor, requestData.witness);
            return new OkObjectResult(transaction);
        }


        
    }

    
}
