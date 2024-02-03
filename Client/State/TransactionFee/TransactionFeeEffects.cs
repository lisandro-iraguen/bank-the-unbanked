using CardanoSharp.Wallet.Extensions;
using CardanoSharp.Wallet.Extensions.Models.Transactions;
using CardanoSharp.Wallet.Models.Addresses;
using Client.State.Transaction;
using Data.Wallet;
using Fluxor;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;


namespace Client.State.TransactionFee
{
    public class TransactionFeeEffects
    {
        private readonly HttpClient Http;

        public TransactionFeeEffects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandlSignTransactionAction(TransactionFeeAction action, IDispatcher dispatcher)
        {
            WalletExtensionState wallet = action.UsedWallet;
            string walletfromTransfer = wallet.LastUsedAddress;
            string walletToTransfer = action.TransferTo;
            string valueToTransfer = (action.TransferAmount * wallet.Lovlace).ToString();

            string url = $"/api/TxFee?walletFrom={walletfromTransfer}&walletTo={walletToTransfer}&value={valueToTransfer}";
            var response = await Http.GetAsync(url);
            float fee = 0;
            if (response.IsSuccessStatusCode)
            {
                fee =(float)await response.Content.ReadFromJsonAsync<ulong>();
                fee = fee/wallet.Lovlace;
                Console.WriteLine($"transaction calculated fee completed: {fee}");
                
            }

            dispatcher.Dispatch(new TransactionFeeResultAction(fee));
        }
    }
}
