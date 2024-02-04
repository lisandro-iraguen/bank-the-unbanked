using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Models.Addresses;
using Data.Wallet;
using Fluxor;
using Newtonsoft.Json;
using Radzen;
using System.Net.Http.Json;
using System.Text;


namespace Client.State.Transaction
{
    public class Effects
    {
        private readonly HttpClient Http;
        private readonly DialogService _dialogService;

        public Effects(HttpClient http, DialogService dialogService)
        {
            Http = http;
            _dialogService = dialogService;
        }

        [EffectMethod]
        public async Task HandlSignTransactionAction(SignTransactionAction action, IDispatcher dispatcher)
        {
            WalletExtensionState wallet = action.UsedWallet;
            string walletfromTransfer = wallet.LastUsedAddress;
            string walletToTransfer = action.TransferTo;
            string valueToTransfer = (action.TransferAmount*wallet.Lovlace).ToString();



            string url = $"/api/TxBuild?walletFrom={walletfromTransfer}&walletTo={walletToTransfer}&value={valueToTransfer}";
            try
            {
                var response = await Http.GetAsync(url);
                var txSignData = new TxRequest();

                if (response.IsSuccessStatusCode)
                {
                    var transaction = await response.Content.ReadFromJsonAsync<CardanoSharp.Wallet.Models.Transactions.Transaction>();
                    txSignData.transactionCbor = transaction.Serialize().ToStringHex();
                    var witnessSet = await wallet.WalletConnectorJs.SignTx(txSignData.transactionCbor, true);
                    txSignData.witness = witnessSet;
                    Console.WriteLine($"transaction Sign Completed");



                    string jsonContent = JsonConvert.SerializeObject(txSignData);
                    StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage response2 = await Http.PostAsync("api/TxSign", content);
                    if (response2.IsSuccessStatusCode)
                    {
                        var result = await response2.Content.ReadFromJsonAsync<CardanoSharp.Wallet.Models.Transactions.Transaction>();
                        var transactionCbor = result.Serialize().ToStringHex();
                        var delivered = await wallet.WalletConnectorJs.SubmitTx(transactionCbor);
                        dispatcher.Dispatch(new SignTransactionResultAction(action.UsedWallet));
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                        dispatcher.Dispatch(new SignTransactionFailedResultAction());
                    }




                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                    dispatcher.Dispatch(new SignTransactionFailedResultAction());
                }
            }
            catch
            {
                dispatcher.Dispatch(new SignTransactionFailedResultAction());
            }

            _dialogService.Close();
        }

    }
}
