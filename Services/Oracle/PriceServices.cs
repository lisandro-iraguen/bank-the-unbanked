using Data.Oracle;
using Newtonsoft.Json;

namespace Api.Services.Oracle
{
    public class PriceServices:IPriceServices
    {
        public async Task<CriptoDTO> DollarApiCall()
        {

            string apiUrl = "https://criptoya.com/api/binancep2p/ada/ars/1";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        CriptoDTO price = JsonConvert.DeserializeObject<CriptoDTO>(result);
                        Console.WriteLine($"TotalBid Price: {price.TotalBid}");
                        return price;

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }

            return null;
        }
    }
}
