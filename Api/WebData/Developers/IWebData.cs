using Data.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.WebData.Developers
{
    public interface IWebData
    {
        Task<IEnumerable<Developer>> GetWebDevelopersData();

    }
}
