using Radzen;

namespace Client.Shared
{
    public partial class NavMenu
    {
        private void goToHome(MenuItemEventArgs e)
        {
            NavigationManager.NavigateTo("/home");
        }

        private void goToAbout(MenuItemEventArgs e)
        {
            NavigationManager.NavigateTo("/about");
        }
    }
}
