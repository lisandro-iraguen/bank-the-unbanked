
using Data.Oracle;
using Fluxor;

namespace Client.State.Crypto
{
    [FeatureState]

    public class CryptoState
    {
        public bool IsLoading { get; }

        public CriptoDTO? Crypto { get; }

        private CryptoState() { }
        public CryptoState(bool isLoading, CriptoDTO crypto)
        {
            IsLoading = isLoading;
			Crypto = crypto ?? null;
        }
    }
}
