using Client.State.Connection;
using Components;
using Fluxor;
using System.Net.Http.Json;
using Data.Wallet;
using Client.State.Crypto;
using Data.Oracle;
using Client.State.Wallet;

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
            dispatcher.Dispatch(new IsConnectedConectionAction());
            var walletConnectorJs = new WalletConnectorJsInterop(action.JsRuntime);
           
            try
            {   
                var extensions = await Http.GetFromJsonAsync<IEnumerable<WalletExtension>>("api/WalletsData");

                if (extensions is not null)
                {
                    var _wallets = await walletConnectorJs.Init(extensions);
                    foreach (var _wallet in _wallets)
                        _wallet.WalletConnectorJs = walletConnectorJs;


                    if (extensions is not null)
                    {
                        dispatcher.Dispatch(new WalletInitializerResultAction(jsInterop: walletConnectorJs, extensions: _wallets!));
                        dispatcher.Dispatch(new WalletConnectAutomaticallyAction(_wallets));

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
           
        }

    }
}
