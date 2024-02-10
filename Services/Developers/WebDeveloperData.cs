

using Data.Web;
namespace Api.Services.Developers;



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
