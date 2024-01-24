using Data.Oracle;
using Data.Web;

namespace Client.State.Crypto
{
    public class FetchCryptoResultAction
    {
        public CriptoDTO criptoDTO { get; }

        public FetchCryptoResultAction(CriptoDTO cto)
        {
			criptoDTO = cto;
        }
    }
}
