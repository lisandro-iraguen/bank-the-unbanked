

using Blazored.LocalStorage;
using CardanoSharp.Wallet.CIPs.CIP30.Models;
using CardanoSharp.Wallet.Enums;
using Components;
using Data.Exceptions;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Data.Wallet
{
    public class WalletConnector
    {
    

		public List<WalletExtensionState>? _wallets = null;
        public WalletConnectorJsInterop? _walletConnectorJs;
        public WalletExtensionState? _connectedWallet { get; set; }

        public List<WalletExtension> SupportedExtensions { get; set; }

        public bool Connecting { get; private set; }
        public bool Initialized { get; private set; }
        public bool Connected { get; private set; }

        private ILocalStorageService _localStorage;
        private HttpClient _http;
        private IJSRuntime _javascriptRuntime;
        public WalletConnector(ILocalStorageService lStorage, HttpClient client, IJSRuntime jSRuntime)
        {
            _localStorage = lStorage;
            _http = client;
            _javascriptRuntime = jSRuntime;
        }
      
        public async Task ConnectWalletAutomatically()
        {
            //var supportedWalletKeys = SupportedExtensions?.Select(s => s.Key)?.ToArray();
            //if (supportedWalletKeys != null && supportedWalletKeys.Length > 0)
            //{
            //    var storedWalletKey = await GetStoredWalletKeyAsync(supportedWalletKeys);

            //    if (!string.IsNullOrWhiteSpace(storedWalletKey))
            //    {
            //        if (!await ConnectWalletAsync(storedWalletKey, false))
            //        {
            //            await RemoveStoredWalletKeyAsync();
            //        }
            //    }
            //}
        }
        public async Task ConnectWallet(string key)
        {
            //var supportedWalletKeys = SupportedExtensions?.Select(s => s.Key)?.ToArray();

            //if (supportedWalletKeys != null && supportedWalletKeys.Length > 0)
            //{
            //    var storedWalletKey = await GetStoredWalletKeyAsync(supportedWalletKeys);
            //    if (string.IsNullOrWhiteSpace(storedWalletKey))
            //    {
            //        if (supportedWalletKeys.Contains(key))
            //        {
            //            if (!await ConnectWalletAsync(key, false))
            //            {
            //                await RemoveStoredWalletKeyAsync();
            //            }
            //        }
            //    }
            //}
        }


        //private async Task<string> GetStoredWalletKeyAsync(params string[] supportedWalletKeys)
        //{
        //    //var result = string.Empty;

        //    //try
        //    //{
        //    //    if (_localStorage != null && supportedWalletKeys != null)
        //    //    {
        //    //        var walletKey = await _localStorage.GetItemAsStringAsync(ComponentUtils.ConnectedWalletKey);

        //    //        if (!string.IsNullOrWhiteSpace(walletKey) && supportedWalletKeys.Any(w => w.Equals(walletKey, StringComparison.OrdinalIgnoreCase)))
        //    //        {
        //    //            result = walletKey;
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception err)
        //    //{
        //    //    Console.WriteLine(err.Message);
        //    //}

        //    //return result;
        //}
        private async Task RemoveStoredWalletKeyAsync()
        {
            //if (_localStorage != null)
            //{
            //    await _localStorage.RemoveItemAsync(ComponentUtils.ConnectedWalletKey);
            //}
       }
       

      
     
        //public async ValueTask<DataSignature> SignData(string address, string hexData)
        //{
        //   
        //    //return await _walletConnectorJs!.SignData(address, hexData);
        //}

        //public async ValueTask<string> SignTx(Transaction tx, bool partialSign = false)
        //{
        //    //var txCbor = tx.Serialize().ToStringHex();
        //    //return await SignTxCbor(txCbor, partialSign);
        //}
        //public async ValueTask<string> SignTxCbor(string txCbor, bool partialSign = false)
        //{
        //    CheckInitializedAndConnected();
        //    Console.WriteLine($"TX CBOR: {txCbor}");
        //    return await _walletConnectorJs!.SignTx(txCbor, partialSign);
        //}

        //public async ValueTask<string> SubmitTx(Transaction tx)
        //{
        //    var txCbor = tx.Serialize().ToStringHex();
        //    return await SubmitTxCbor(txCbor);
        //}
        //public async ValueTask<string> SubmitTxCbor(string txCbor)
        //{
        //    CheckInitializedAndConnected();
        //    Console.WriteLine(($"TX CBOR: {txCbor}"));
        //    return await _walletConnectorJs!.SubmitTx(txCbor);
        //}

        //private async Task SetStoredWalletKeyAsync(string walletKey)
        //{
        //    if (_localStorage != null && !string.IsNullOrWhiteSpace(walletKey))
        //    {
        //        await _localStorage.SetItemAsStringAsync(ComponentUtils.ConnectedWalletKey, walletKey);
        //    }
        //}
      

        //public async ValueTask<Utxo[]> GetUtxos(TransactionOutputValue? requiredOutput = null, Paginate? paginate = null)
        //{
        //    string? amountCbor = null;
        //    if (requiredOutput != null)
        //    {
        //        amountCbor = requiredOutput.Serialize().ToStringHex();
        //    }
        //    var utxoCbors = await GetUtxosCbor(amountCbor, paginate);
        //    var utxoList = new List<Utxo>();
        //    foreach (var utxoCbor in utxoCbors)
        //    {
        //        try
        //        {
        //            utxoList.Add(utxoCbor.HexToByteArray().DeserializeUtxo());
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new Exception($"error during utxo deserialization {ex}{utxoCbor}");
        //        }
        //    }
        //    return utxoList.ToArray();
        //}

        //public async ValueTask<string[]> GetUtxosCbor(string? requiredOutputCbor = null, Paginate? paginate = null)
        //{
        //    CheckInitializedAndConnected();
        //    var utxoCbors = await _walletConnectorJs!.GetUtxos(requiredOutputCbor, paginate);
        //    foreach (var utxoCbor in utxoCbors)
        //    {
        //        Console.WriteLine($"UTXO CBOR: {utxoCbor}");
        //    }
        //    return utxoCbors;
        //}

      
    }
}
