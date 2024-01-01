using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;


namespace Client.Pages
{
    public partial class DeFi
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        private readonly string miniSwap = "https://app.minswap.org/";

        private async Task goToMiniSwap()
        {
            await JSRuntime.InvokeAsync<object>("open", miniSwap, "_blank");
        }
    }
}
