using Radzen;

namespace Client.Shared
{
    public partial class NavMenu
    {
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
