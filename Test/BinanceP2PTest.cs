using Api.Services.Oracle;
using Api.Wallet;
using Api.WebData.Developers;
using CardanoSharp.Wallet;
using Data.Wallet;
using Data.Web;

namespace Test
{
    public class BinanceP2PTest
    {
        private IPriceServices oracle;


        [SetUp]
        public void Setup()
        {
            oracle = new PriceServices();

        }

        [Test]
        public async Task GetPriceServices_ShouldReturnPrice()
        {            
            var result = await oracle.DollarApiCall();

            Assert.IsNotNull(result);
            
            Assert.IsNotNull(5, result.DTTime.ToString());
            Assert.IsTrue(result.TotalBid>0, "it is bigger than 0");


      

        }

    }
}