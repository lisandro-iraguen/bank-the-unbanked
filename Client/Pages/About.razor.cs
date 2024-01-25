using Client.State.Developer;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using System;

namespace Client.Pages
{
    public partial class About: FluxorComponent
    {
        [Inject]
        protected IDispatcher dispatcher { get; set; }  

        [Inject]
        protected IState<DeveloperState> developersState { get; set; }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new FetchDeveloperAction());
        }
    }
}
