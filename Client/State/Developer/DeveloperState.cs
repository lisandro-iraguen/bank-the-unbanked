using Data.Web;
using Fluxor;

namespace Client.State.Developer
{
    [FeatureState]

    public class CryptoState
    {
        public bool IsLoading { get; }
        public IEnumerable<Data.Web.Developer> developers { get; }

        private CryptoState() { }
        public CryptoState(bool isLoading, IEnumerable<Data.Web.Developer> devs)
        {
            IsLoading = isLoading;
            developers = devs ?? Array.Empty<Data.Web.Developer>();
        }
    }
}
