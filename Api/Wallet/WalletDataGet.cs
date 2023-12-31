using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace Api.Wallet
{
    public class WalletDataGet
    {
        private readonly IWalletData _walletData;

        public WalletDataGet(IWalletData w)
        {
            _walletData = w;
        }

        [FunctionName("WalletsData")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "WalletsData")] HttpRequest req)
        {
            var products = await _walletData.GetWalletData();
            return new OkObjectResult(products);
        }
    }
}
