using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Extensions.Models.Transactions.TransactionWitnesses;
using Data.Wallet;

namespace Api.Wallet
{
    public class WitnessGet
    {

       

        [FunctionName("Witness")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "WitnessGet")] AddWitnessRequest request)
        {
            var transaction = request.TxCbor;
            if (transaction == null)
            {
                throw new InvalidOperationException("Could not deserialize txCbor");
            }

            var witnessSet = request.WitnessCbor.HexToByteArray().DeserializeTransactionWitnessSet();

            foreach (var vkeyWitness in witnessSet.VKeyWitnesses)
            {
                transaction.TransactionWitnessSet.VKeyWitnesses.Add(vkeyWitness);
            }
            foreach (var nativeScript in witnessSet.NativeScripts)
            {
                transaction.TransactionWitnessSet.NativeScripts.Add(nativeScript);
            }
            foreach (var bootstrap in witnessSet.BootStrapWitnesses)
            {
                transaction.TransactionWitnessSet.BootStrapWitnesses.Add(bootstrap);
            }

            var response = new AddWitnessResponse();
            response.Request = request;
            response.TxCbor = transaction;

            return new OkObjectResult(response);
        }


        
    }
}
