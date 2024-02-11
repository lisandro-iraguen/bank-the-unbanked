using Blazored.LocalStorage;
using Client.State.Connection;
using Client.State.Wallet;
using Client.State.WalletExtensions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;


namespace Client.Shared
{
    public partial class TransactionPopUp
    {


        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }



        private I18nText.Web? webText;

        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);

        }

    }



}
