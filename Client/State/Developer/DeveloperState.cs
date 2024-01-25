using Data.Web;
using Fluxor;

namespace Client.State.Developer
{
    [FeatureState]

    public class DeveloperState
    {
        public bool IsLoading { get; }
        public IEnumerable<Data.Web.Developer> developers { get; }

        private DeveloperState() { }
        public DeveloperState(bool isLoading, IEnumerable<Data.Web.Developer> devs)
        {
            IsLoading = isLoading;
            developers = devs ?? Array.Empty<Data.Web.Developer>();
        }
    }
}
