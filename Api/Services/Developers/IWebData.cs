

using Data.Web;

namespace Api.Services.Developers
{
    public interface IWebData
    {
        Task<IEnumerable<Developer>> GetWebDevelopersData();

    }
}
