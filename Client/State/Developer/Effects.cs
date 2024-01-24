using Data.Web;
using Fluxor;
using System.Net.Http.Json;

namespace Client.State.Developer
{
    public class Effects
    {
        private readonly HttpClient Http;

        public Effects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleFetchDataAction(FetchDeveloperAction action, IDispatcher dispatcher)
        {
            var forecasts = await Http.GetFromJsonAsync<Data.Web.Developer[]>("api/Developers");
            if (forecasts is not null)
            {
                dispatcher.Dispatch(new FetchDeveloperResultAction(devs: forecasts!));
            }
        }

    }
}
