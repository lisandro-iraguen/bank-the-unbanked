using Components;
using Data.Wallet;
using Fluxor;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Client.State.Wallet.Extension
{
    public class Effects
    {
        private readonly HttpClient Http;

        public Effects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleFetchDataAction(FetchExtensionAction action, IDispatcher dispatcher)
        {

            var walletConnectorJs = new WalletConnectorJsInterop(action.JavascriptRuntime);
            var extensions = await Http.GetFromJsonAsync<IEnumerable<WalletExtensionState>>("api/WalletsData");
            var _wallets = await walletConnectorJs.Init(extensions);

            if (extensions is not null)
            {
                dispatcher.Dispatch(new FetchExtensionResultAction(ext: _wallets!));
            }
        }

    }
}
