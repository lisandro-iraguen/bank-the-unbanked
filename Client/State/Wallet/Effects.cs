using Client.State.Wallet.Extension;
using Components;
using Data.Wallet;
using Fluxor;
using System.Net.Http.Json;


namespace Client.State.Wallet
{
    public class Effects
    {
        private readonly HttpClient Http;

        public Effects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleFtechConnectAction(WalletConnectAction action, IDispatcher dispatcher)
        {

            var walletConnectorJs = new WalletConnectorJsInterop(action.JsRuntime);
            var extensions = await Http.GetFromJsonAsync<IEnumerable<WalletExtensionState>>("api/WalletsData");
            var _wallets = await walletConnectorJs.Init(extensions);

            if (extensions is not null)
            {
                dispatcher.Dispatch(new WalletFetchExtensionResultAction(ext: _wallets!));
            }
        }

    }
}
