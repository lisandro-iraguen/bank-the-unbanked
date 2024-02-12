using Api.Services.Developers;
using Data.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace ApiNet8.Controllers
{
    public class DevelopersController
    {
        private readonly ILogger _logger;
        private readonly IWebData _webDevelopers;

        public DevelopersController(ILoggerFactory loggerFactory, IWebData webDevelopersData)
        {
            _logger = loggerFactory.CreateLogger<DevelopersController>();
            _webDevelopers = webDevelopersData;
        }

        [Function("Developers")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request. Developers Function");
            var webDevelopers = _webDevelopers.GetWebDevelopersData();
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            response.WriteString(JsonConvert.SerializeObject(webDevelopers.Result));
            return response;
        }


    }
}
