using Microsoft.JSInterop;
using Data;

namespace Client.Components
{
    public class WalletConnectorJsInterop : IAsyncDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
        private IJSObjectReference? _jsWalletConnector;
        public List<WalletExtensionState>? wallets { get; private set; }

        public WalletConnectorJsInterop(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _moduleTask = new(() => _jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./js/WalletConnectorJsInterop.js").AsTask());
        }

        public async ValueTask<List<WalletExtensionState>> Init(IEnumerable<WalletExtension> supportedWallets)
        {
            Console.WriteLine(_moduleTask.Value);
            var module = await _moduleTask.Value;
            _jsWalletConnector = await module.InvokeAsync<IJSObjectReference>("createWalletConnector");

            //await _jsWalletConnector.InvokeVoidAsync("init", walletObj);

            wallets = new List<WalletExtensionState>();
            foreach (var wallet in supportedWallets)
            {
                var walletState = new WalletExtensionState(wallet);
                if (!String.IsNullOrEmpty(walletState.Key))
                {
                    walletState.Installed = await IsWalletInstalled(walletState.Key);
                    //if (walletState.Installed)
                    //    walletState.Version = await GetWalletApiVersion(walletState.Key);
                    wallets.Add(walletState);
                }
            }

            return wallets;
        }
        public async ValueTask<bool> ConnectWallet(string key)
        {

            try
            {
                return await _jsWalletConnector!.InvokeAsync<bool>("connectWallet", key);
            }
            catch (JSException ex)
            {
                throw new JSException(ex.Message);
            }
        }


        public async ValueTask DisposeAsync()
        {
            if (_jsWalletConnector != null)
            {
                await _jsWalletConnector.DisposeAsync();
            }

            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        public async ValueTask<bool> IsWalletInstalled(string key)
        {
            try
            {
                return await _jsWalletConnector!.InvokeAsync<bool>("isWalletInstalled", key);
            }
            catch (JSException ex)
            {
                throw new JSException(ex.Message);
            }

        }

        public async ValueTask<bool> IsWalletEnabled(string key)
        {
            try
            {
                return await _jsWalletConnector!.InvokeAsync<bool>("isWalletEnabled", key);
            }
            catch (JSException ex)
            {
                throw new JSException(ex.Message);
            }
        }
    }
}
