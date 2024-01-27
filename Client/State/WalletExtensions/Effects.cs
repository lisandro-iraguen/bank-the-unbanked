using Components;
using Data.Wallet;
using Fluxor;
using System.Net.Http.Json;


namespace Client.State.WalletExtensions
{
    public class Effects
    {
        private readonly HttpClient Http;

        public Effects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleWalletInitializerAction(WalletInitializerAction action, IDispatcher dispatcher)
        {

            var walletConnectorJs = new WalletConnectorJsInterop(action.JsRuntime);
            var extensions = await Http.GetFromJsonAsync<IEnumerable<WalletExtensionState>>("api/WalletsData");
            var _wallets = await walletConnectorJs.Init(extensions);

            foreach(var _wallet in _wallets)
                _wallet.WalletConnectorJs = walletConnectorJs;
            

            if (extensions is not null)
            {
                dispatcher.Dispatch(new WalletInitializerResultAction(jsInterop: walletConnectorJs, extensions: _wallets!));
            }
        }

    }
}
