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
        protected IDispatcher? dispatcher { get; set; }  

        [Inject]
        protected IState<DeveloperState>? developersState { get; set; }

        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }
        private I18nText.Web? webText;

        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);

        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            dispatcher.Dispatch(new FetchDeveloperAction());
        }
    }
}
