using Microsoft.JSInterop;
using CardanoSharp.Wallet.CIPs.CIP30.Models;
using Data.Wallet;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Data.Exceptions;
using Data.Errors;


namespace Components
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
        public async ValueTask<string> GetBalance()
        {
         
            try
            {
                var balance = await _jsWalletConnector!.InvokeAsync<string>("getBalance");
                return balance;
            }
            catch (JSException ex)
            {
                throw new JSException(ex.Message);
            }
        }


        public async ValueTask<int> GetNetworkId()
        {
            try
            {
                var networkId = await _jsWalletConnector!.InvokeAsync<int>("getNetworkId");
                return networkId;
            }
            catch (JSException ex)
            {
                throw new JSException(ex.Message);
            }
        }

        public async ValueTask<string[]> GetUsedAddresses(Paginate? paginate = null)
        {
           
            try
            {
                var addresses = await _jsWalletConnector!.InvokeAsync<string[]>("getUsedAddresses", paginate);
                return addresses;
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

        public async ValueTask<DataSignature> SignData(string address, string hexData)
        {
            
            try
            {
                var dataSignature = await _jsWalletConnector!.InvokeAsync<DataSignature>("signData", address, hexData);
                return dataSignature;
            }
            catch (JSException ex)
            {
                throw ex.ToWebWalletException(WebWalletErrorType.DataSign);
            }
        }

        public async ValueTask<string> SignTx(string txCbor, bool partialSign = false)
        {
           
            try
            {
                var cborWitnessSet = await _jsWalletConnector!.InvokeAsync<string>("signTx", txCbor, partialSign);
                return cborWitnessSet;
            }
            catch (JSException ex)
            {
                throw ex.ToWebWalletException(WebWalletErrorType.TxSign);
            }
        }

        public async ValueTask<string> SubmitTx(string txCbor)
        {
          
            try
            {
                var txHash = await _jsWalletConnector!.InvokeAsync<string>("submitTx", txCbor);
                return txHash;
            }
            catch (JSException ex)
            {
                throw ex.ToWebWalletException(WebWalletErrorType.TxSign);
            }
        }
        public async ValueTask<string[]> GetUtxos(string? amountCbor = null, Paginate? paginate = null)
        {
             try
            {
                var utxos = await _jsWalletConnector!.InvokeAsync<string[]>("getUtxos", amountCbor, paginate);
                return utxos;
            }
            catch (JSException ex)
            {
                throw new JSException(ex.Message);
            }
        }
    }
}
