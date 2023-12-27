using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api
{
    public class PostGet
    {
        private readonly IProductData _productData;

        public PostGet(IProductData productData)
        {
            this._productData = productData;
        }

        [FunctionName("PostGet")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "posts")] HttpRequest req)
        {
            var products = await this._productData.GetProducts();
            return new OkObjectResult(products);
        }
    }
}
