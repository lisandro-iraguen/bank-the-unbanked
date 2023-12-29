using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Web;

namespace Api.WebData.Developers;



public class WebDeveloperData : IWebData
{
    private readonly List<Developer> walletsData = new List<Developer>
        {
            new Developer
            {
                   Text = "Lisandro Iraguen",
                    Url = "https://www.linkedin.com/in/lisandroiraguen/"

            }

        };



    public Task<IEnumerable<Developer>> GetWebDevelopersData()
    {
        return Task.FromResult(walletsData.AsEnumerable());
    }


}
