using Data.Oracle;
using Data.Web;
using Fluxor;
using System.Net.Http.Json;

namespace Client.State.Crypto
{
    public class Effects
    {
        private readonly HttpClient Http;

        public Effects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleFetchDataAction(FetchCryptoAction action, IDispatcher dispatcher)
        {
            var crypto = await Http.GetFromJsonAsync<CriptoDTO>("api/BinanceP2P");
            if (crypto is not null)
            {
                dispatcher.Dispatch(new FetchCryptoResultAction(cto: crypto!));
            }
        }

    }
}
