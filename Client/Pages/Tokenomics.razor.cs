using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Client.Pages
{
    public partial class Tokenomics
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        private readonly string tokenomics = "https://preview.cexplorer.io/policy/797828bae21c7dcbad5015d31380eb52632c04bb875a4c77fe16b5db";
        private readonly string WhitePapper = "https://localhost:44348/documents/ArgentinePesoWithCardanoBlockch.pdf";

        private async Task toToTokenomics()
        {
            await JSRuntime.InvokeAsync<object>("open", tokenomics, "_blank");
        }
        
        private async Task downloadWhitePapper()
        {
            await JSRuntime.InvokeAsync<object>("open", WhitePapper, "_blank");
        }

        
    }
}
