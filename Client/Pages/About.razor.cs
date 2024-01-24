using Client.State.Developer;

namespace Client.Pages
{
    public partial class About
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new FetchDeveloperAction());
        }
    }
}
