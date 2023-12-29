using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Api.WebData.Developers;

namespace Api.Web
{
    public class WebDataGet
    {
        private readonly IWebData _webData;

        public WebDataGet(IWebData w)
        {
            _webData = w;
        }

        [FunctionName("WebData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "developers")] HttpRequest req)
        {
            var developers = await _webData.GetWebDevelopersData();
            return new OkObjectResult(developers);
        }
    }
}
