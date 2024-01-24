using Data.Web;

namespace Client.State.Developer
{
    public class FetchDeveloperResultAction
    {
        public IEnumerable<Data.Web.Developer> developers { get; }

        public FetchDeveloperResultAction(IEnumerable<Data.Web.Developer> devs)
        {
            developers = devs;
        }
    }
}
