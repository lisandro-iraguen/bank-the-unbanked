using Client.State.Connection;
using Client.State.Wallet;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace Client.Shared.Wallet
{
    public partial class WalletInformation
    {
        [Inject] IState<ConectedState>? walletConecting { get; set; }
        [Inject] IState<WalletExtensions>? walletState { get; set; }

        [Inject] Toolbelt.Blazor.I18nText.I18nText I18nText { get; set; }

        private I18nText.Web? webText;
        protected override async Task OnInitializedAsync()
        {
            webText = await I18nText.GetTextTableAsync<I18nText.Web>(this);
         
        }
    }
}
