using Fluxor;
using System.Net.Http.Json;
using CardanoSharp.Koios.Client.Contracts;
using Newtonsoft.Json;
using Data.History;

namespace Client.State.WalletHistory
{
    public class WalletHistoryEffects
    {
        private readonly HttpClient Http;


        public WalletHistoryEffects(HttpClient http)
        {
            Http = http;
        }

        [EffectMethod]
        public async Task HandleWalletHistoryConnectorAction(WalletHistoryConnectorAction action, IDispatcher dispatcher)
        {
            
            var wallet= action.Wallet.LastUsedAddress;
            var url = "api/TxHistory?walletFrom=" + wallet; 
            var response = await Http.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var transactionsRaw = await response.Content.ReadAsStringAsync();
                var transactions = JsonConvert.DeserializeObject<TxHistory[]>(transactionsRaw);
                dispatcher.Dispatch(new WalletHistoryConnectorResultAction(transactions: transactions!));
            }


        }





    }
}
