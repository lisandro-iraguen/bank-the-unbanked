using Api.Services.Wallet;
using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions.TransactionWitnesses;
using Data.Wallet;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ApiNet8.Controllers
{
    public class WalletController
    {
        private readonly ILogger _logger;
        private readonly IWalletData _walletData;

        public WalletController(ILoggerFactory loggerFactory, IWalletData walletData)
        {
            _logger = loggerFactory.CreateLogger<WalletController>();
            _walletData = walletData;
        }

        [Function("WalletsData")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var products = await _walletData.GetWalletData();
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            response.WriteString(JsonConvert.SerializeObject(products));
            return response;
        }


        [Function("WitnessGet")]
        public async Task<HttpResponseData> RunAsync2([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, [FromBody] AddWitnessRequest request)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

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
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");

            var result = new AddWitnessResponse();
            result.Request = request;
            result.TxCbor = transaction;
            var responseContent = JsonConvert.SerializeObject(result);
            await response.WriteStringAsync(responseContent);
            return response;
        }

    }
}
