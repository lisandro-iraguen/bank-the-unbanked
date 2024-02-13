using Api.Services.Oracle;
using Api.Services.Policy;
using Api.Services.Transaction;
using Api.Services.Wallet;
using CardanoSharp.Koios.Client;
using CardanoSharp.Koios.Client.Contracts;
using Microsoft.Extensions.Configuration;
using Moq;
using Refit;
using System.Net;

namespace Test
{
    public class CryptoPriceTests
    {
        private IPriceServices priceService;


        [SetUp]
        public void Setup()
        {
          

            priceService = new PriceServices();
        }

        [Test]
        public async Task GePriceSerivices_ShouldReturnNotNullValue()
        {
            var result = await priceService.DollarApiCall();
            Assert.IsNotNull(result);

        }

    }
}