using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Pages
{
    public partial class Tokenomics
    {
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }
        [Inject]
        protected IConfiguration? _configuration { get; set; }



        private string? tokenomicsURl;
        private string? whitePapperUrl;




        protected override Task OnInitializedAsync()
        {
            tokenomicsURl = _configuration.GetValue<string>("AppSettings:TokenomicsURL");
            whitePapperUrl = _configuration.GetValue<string>("AppSettings:WhitePapperUrl");
            return Task.CompletedTask;
        }

        private async Task toToTokenomics()
        {
            await JSRuntime.InvokeAsync<object>("open", tokenomicsURl, "_blank");
        }
        
        private async Task downloadWhitePapper()
        {
            await JSRuntime.InvokeAsync<object>("open", whitePapperUrl, "_blank");
        }

        
    }
}
