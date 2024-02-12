using Api.Services.Developers;
using Api.Services.Oracle;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ApiNet8.Controllers
{
    public class WebPriceController
    {
        private readonly ILogger _logger;
        private readonly IPriceServices _webPriceSerices;

        public WebPriceController(ILoggerFactory loggerFactory, IPriceServices webPriceSerices)
        {
            _logger = loggerFactory.CreateLogger<WebPriceController>();
            _webPriceSerices = webPriceSerices;
        }

        [Function("BinanceP2P")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request. BinanceP2P Function");
            var webDevelopers = _webPriceSerices.DollarApiCall();
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            response.WriteString(JsonConvert.SerializeObject(webDevelopers.Result));
            return response;
        }
    }
}
