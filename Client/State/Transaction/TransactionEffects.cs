using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Models.Addresses;
using Data.Wallet;
using Fluxor;
using System.Net.Http.Json;


namespace Client.State.Transaction
{
    public class Effects
    {
        private readonly HttpClient Http;

        public Effects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandlSignTransactionAction(SignTransactionAction action, IDispatcher dispatcher)
        {
            WalletExtensionState wallet = action.UsedWallet;         
            string walletfromTransfer = wallet.LastUsedAddress;
            string walletToTransfer = action.TransferTo;
            string valueToTransfer = action.TransferAmount.ToString();

            


            string url = $"/api/TxBuild?walletFrom={walletfromTransfer}&walletTo={walletToTransfer}&value={valueToTransfer}";
            var response = await Http.GetAsync(url);
            var txSignData = new TxRequest();

            if (response.IsSuccessStatusCode)
            {
                var transaction = await response.Content.ReadFromJsonAsync<CardanoSharp.Wallet.Models.Transactions.Transaction>();
                txSignData.transactionCbor = transaction.Serialize().ToStringHex();
                var witnessSet = await wallet.WalletConnectorJs.SignTx(txSignData.transactionCbor, true);
                txSignData.witness = witnessSet;
                Console.WriteLine($"transaction compelted");
                //dispatcher.Dispatch(new SignTransactionResultAction(wallet));
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

    }
}
