using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Client.Pages
{
    public partial class Tokenomics
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected IConfiguration _configuration { get; set; }



        private string tokenomicsURl;
        private string whitePapperUrl;




        protected override async Task OnInitializedAsync()
        {
            tokenomicsURl = _configuration.GetValue<string>("AppSettings:TokenomicsURL");
            whitePapperUrl = _configuration.GetValue<string>("AppSettings:WhitePapperUrl");
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
