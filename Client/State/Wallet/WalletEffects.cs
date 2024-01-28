﻿using Blazored.LocalStorage;
using CardanoSharp.Wallet.CIPs.CIP30.Models;
using CardanoSharp.Wallet.Enums;
using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Models.Transactions;
using Client.State.WalletConnecting;
using Components;
using Data.Wallet;
using Fluxor;
using PeterO.Cbor2;
using System.Net.Http.Json;
using Utils;


namespace Client.State.Wallet
{
    public class WalletEffects
    {
        private readonly HttpClient Http;


        public WalletEffects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleWalletInitializerAction(WalletConnectorAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new ChangeConnectingStateAction(true));
            action.DialogService.Close();
            string key = action.Key;
            var valletList = action.Wallets;

            var supportedWalletKeys = SupportedWalletListToString(valletList);
            if (supportedWalletKeys.Contains(key))
            {
                var walletSelected = GetWalletSelected(key, valletList);
                var result = await GetConnectionResult(key, walletSelected);
                if (result)
                {
                    walletSelected = await UpdateWallet(key, walletSelected);
                    dispatcher.Dispatch(new WalletConnectorResultAction(wallet: walletSelected));
                    dispatcher.Dispatch(new ChangeConnectingStateAction(false));
                    action.DialogService.Refresh();
                }
            }


        }




        [EffectMethod]
        public async Task HandleWalletConnectAutomaticallyAction(WalletConnectAutomaticallyAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new ChangeConnectingStateAction(true));
            var valletList = action.Wallets;
            var supportedWalletKeys = SupportedWalletListToString(valletList);
            var storedWalletKey = await GetStoredWalletKeyAsync(supportedWalletKeys, action.LocalStorageSerivce);
            if (String.IsNullOrEmpty(storedWalletKey))
            {
                dispatcher.Dispatch(new ChangeConnectingStateAction(false));
                Console.WriteLine($"Key not found {storedWalletKey}");
            }
            else
            {
                if (supportedWalletKeys.Contains(storedWalletKey))
                {
                    var walletSelected = GetWalletSelected(storedWalletKey, valletList);
                    var result = await GetConnectionResult(storedWalletKey, walletSelected);
                    if (result)
                    {
                        walletSelected = await UpdateWallet(storedWalletKey, walletSelected);
                        dispatcher.Dispatch(new WalletConnectorResultAction(wallet: walletSelected));
                        dispatcher.Dispatch(new ChangeConnectingStateAction(false));
                        action.DialogService.Refresh();
                    }
                }
            }
        }




        private async Task<WalletExtensionState> UpdateWallet(string key, WalletExtensionState walletSelected)
        {
            Console.WriteLine($"wallet conected {key} sucessful!");
            walletSelected.Connected = true;
            var balance = await GetBalance(walletSelected.WalletConnectorJs);
            if (balance != null)
            {
                walletSelected!.TokenCount = 0;
                walletSelected.TokenPreservation = 0;
                walletSelected.NativeAssets = new();
                if (balance.MultiAsset != null && balance.MultiAsset.Count > 0)
                {
                    walletSelected.TokenPreservation = balance.MultiAsset.CalculateMinUtxoLovelace();
                    walletSelected.TokenCount = balance.MultiAsset.Sum(x => x.Value.Token.Keys.Count);
                    walletSelected.NativeAssets = balance.MultiAsset.SelectMany(policy =>
                           policy.Value.Token.Select(asset =>
                               new KeyValuePair<string, ulong>(
                                   $"{policy.Key.ToStringHex()}-{asset.Key.ToStringHex()}",
                                   (ulong)asset.Value)))
                       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                }
                walletSelected.Balance = balance.Coin - walletSelected.TokenPreservation;
                walletSelected.UsedAdress = await GetUsedAddressesHex(walletSelected.WalletConnectorJs);
            }
            walletSelected!.Network = await GetNetworkType(walletSelected.WalletConnectorJs);
            return walletSelected;
        }
        public async ValueTask<TransactionOutputValue> GetBalance(WalletConnectorJsInterop _walletConnectorJs)
        {
            var result = await GetBalanceCbor(_walletConnectorJs);
            var cborObj = CBORObject.DecodeFromBytes(result.HexToByteArray());
            if (cborObj.Type == CBORType.Integer)
            {
                var number = cborObj.AsNumber();
                var coin = number.ToUInt64Checked();
                return new TransactionOutputValue() { Coin = coin };
            }

            var outputValue = result.HexToByteArray().DeserializeTransactionOutputValue();
            return outputValue;
        }
        public async ValueTask<string> GetBalanceCbor(WalletConnectorJsInterop _walletConnectorJs)
        {
            var result = await _walletConnectorJs!.GetBalance();
            Console.WriteLine($"BALANCE CBOR: {result}");
            return result;
        }

        public async ValueTask<NetworkType> GetNetworkType(WalletConnectorJsInterop _walletConnectorJs)
        {
            var networkId = await GetNetworkTypeId(_walletConnectorJs);
            return ComponentUtils.GetNetworkType(networkId);

        }
        public async ValueTask<int> GetNetworkTypeId(WalletConnectorJsInterop _walletConnectorJs)
        {

            var networkId = await _walletConnectorJs!.GetNetworkId();
            Console.WriteLine($"NETWORK ID: {networkId}");
            return networkId;

        }

        public async ValueTask<string[]> GetUsedAddressesHex(WalletConnectorJsInterop _walletConnectorJs, Paginate? paginate = null)
        {
            var addresses = await _walletConnectorJs!.GetUsedAddresses(paginate);
            foreach (var address in addresses)
            {
                Console.WriteLine($"USED ADDRESS: {address}");
            }
            return addresses;
        }

        private async Task<string> GetStoredWalletKeyAsync(string[] supportedWalletKeys, ILocalStorageService localStorage)
        {
            var result = string.Empty;

            try
            {
                if (localStorage != null && supportedWalletKeys != null)
                {
                    var walletKey = await localStorage.GetItemAsStringAsync(ComponentUtils.ConnectedWalletKey);

                    if (!string.IsNullOrWhiteSpace(walletKey) && supportedWalletKeys.Any(w => w.Equals(walletKey, StringComparison.OrdinalIgnoreCase)))
                    {
                        result = walletKey;
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }

            return result;
        }
        private async Task RemoveStoredWalletKeyAsync(ILocalStorageService localStorage)
        {
            if (localStorage != null)
            {
                await localStorage.RemoveItemAsync(ComponentUtils.ConnectedWalletKey);
            }
        }

        private string[]? SupportedWalletListToString(IEnumerable<WalletExtensionState> valletList)
        {
            return valletList?.Select(s => s.Key)?.ToArray();
        }
        private WalletExtensionState GetWalletSelected(string key, IEnumerable<WalletExtensionState> valletList)
        {
            return valletList!.First(x => x.Key == key);
        }
        private async Task<bool> GetConnectionResult(string key, WalletExtensionState walletSelected)
        {
            return await walletSelected.WalletConnectorJs!.ConnectWallet(key);
        }
    }
}
