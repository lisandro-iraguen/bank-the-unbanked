using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.Services.Transaction;

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
            var transact = await _transaction.BuildTransaction("addr_test1qzfspdp2s5rmecrusgawm7hpkm8qpdksxjtymxz2aa0yyd5lazyya790dh0xwfmcjyjjwc967z62wp8vmkza3pu8l0nq30pne3");
            return new OkObjectResult(transact);
        }


        
    }
}
