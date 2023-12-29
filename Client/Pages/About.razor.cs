using Client.Components;
using Data.Wallet;
using Data.Web;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Client.Pages
{
    public partial class About
    {
        [Inject]
        protected HttpClient http { get; set; }

        private List<Developer>? _developers = null;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                IEnumerable<Developer> developers = await http.GetFromJsonAsync<IEnumerable<Developer>>("api/developers");
                _developers = developers.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        
        }
    }
}
