
using Api.Services.Oracle;

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