using Api.Services.Oracle;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;

namespace Api.Web
{
    public class WebPriceGet
    {
        private readonly IPriceServices _priceServices;

        public WebPriceGet(IPriceServices p)
        {
            _priceServices = p;
        }

        [FunctionName("WebPrice")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "BinanceP2P")] HttpRequest req)
        {
            var adaPrice= await _priceServices.DollarApiCall();
            return new OkObjectResult(adaPrice);
        }
    }
}
