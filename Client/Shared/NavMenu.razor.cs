using Microsoft.AspNetCore.Components;
using Radzen;

namespace Client.Shared
{
    public partial class NavMenu
    {
        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }
        private I18nText.Web? webText;

        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);

        }
            private void goToHome(MenuItemEventArgs e)
        {
            NavigationManager.NavigateTo("/home");
        } 
        
        private void goToDeFi(MenuItemEventArgs e)
        {
            NavigationManager.NavigateTo("/defi");
        } 
        private void goToTokenomics(MenuItemEventArgs e)
        {
            NavigationManager.NavigateTo("/tokenomics");
        }

        private void goToAbout(MenuItemEventArgs e)
        {
            NavigationManager.NavigateTo("/about");
        }
    }
}
