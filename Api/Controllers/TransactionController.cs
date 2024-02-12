using Api.Services.Developers;
using Api.Services.Transaction;
using Azure;
using Data.Wallet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ApiNet8.Controllers
{
    public class TransactionController
    {
        private readonly ILogger _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILoggerFactory loggerFactory, ITransactionService transactionService)
        {
            _logger = loggerFactory.CreateLogger<TransactionController>();
            _transactionService = transactionService;
        }

        [Function("TxBuild")]
        public async Task<HttpResponseData> RunTxBuilderAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {            
            _logger.LogInformation("C# HTTP trigger function processed a request. Transaction Build Function");
            string walletFrom = req.Query["walletFrom"];
            string walletTo = req.Query["walletto"];
            string value = req.Query["value"];
            var response = req.CreateResponse(HttpStatusCode.OK);


            if (string.IsNullOrEmpty(walletFrom)) response = req.CreateResponse(HttpStatusCode.BadRequest);            
            if (string.IsNullOrEmpty(walletTo))   response = req.CreateResponse(HttpStatusCode.BadRequest);            
            if (string.IsNullOrEmpty(value))      response = req.CreateResponse(HttpStatusCode.BadRequest);
            
            try
            {
                ulong transferValue = ulong.Parse(value);
                var transact = await _transactionService.BuildTransaction(walletFrom, walletTo, transferValue);
                response.Headers.Add("Content-Type", "application/json");
                response.WriteString(JsonConvert.SerializeObject(transact));
                return response;
            }
            catch (Exception e)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;
            
        }

        [Function("TxFee")]
        public async Task<HttpResponseData> RunTxFeeAsync([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request. Transaction Fee Function");
            string walletFrom = req.Query["walletFrom"];
            string walletTo = req.Query["walletto"];
            string value = req.Query["value"];
            var response = req.CreateResponse(HttpStatusCode.OK);


            if (string.IsNullOrEmpty(walletFrom)) response = req.CreateResponse(HttpStatusCode.BadRequest);
            if (string.IsNullOrEmpty(walletTo)) response = req.CreateResponse(HttpStatusCode.BadRequest);
            if (string.IsNullOrEmpty(value)) response = req.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                ulong transferValue = ulong.Parse(value);
                var fee = await _transactionService.CalculateFee(walletFrom, walletTo, transferValue);
                response.Headers.Add("Content-Type", "application/json");
                response.WriteString(JsonConvert.SerializeObject(fee));
                return response;
            }
            catch (Exception e)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;

        }


        [Function("TxSign")]
        public async Task<HttpResponseData> RunTxSignAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.  Transaction Sign Function");
            var response = req.CreateResponse(HttpStatusCode.OK);

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var requestData = JsonConvert.DeserializeObject<TxRequest>(requestBody);
                var transaction = await _transactionService.SignTransaction(requestData.transactionCbor, requestData.witness);
                response.Headers.Add("Content-Type", "application/json");
                response.WriteString(JsonConvert.SerializeObject(transaction));
                return response;
            }
            catch (Exception e)
            {
                response = req.CreateResponse(HttpStatusCode.BadRequest);
            }

            return response;

        }

    }
}
