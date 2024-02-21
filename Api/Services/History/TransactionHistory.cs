
using Microsoft.Extensions.Configuration;


namespace Api.Services.History
{ 
    public class TransactionHistory:ITransactionHistory
    {
        private readonly IConfiguration _configuration;

        public TransactionHistory(IConfiguration configuration)
        {
            _configuration = configuration;
            CardanoScanUrl = _configuration["cardanoScanUrl"];
            CardanoScanUrl += "/transaction/";
        }

        public string CardanoScanUrl { get; }

        public string GetCardanoScanUrl()
        {
            return CardanoScanUrl;
        }
    }
}
