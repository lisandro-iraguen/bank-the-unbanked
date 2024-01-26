using Microsoft.JSInterop;

namespace Client.State.Wallet.Extension
{
    public class FetchExtensionAction
    {
        public IJSRuntime? JavascriptRuntime { get; }

        private FetchExtensionAction() { }
        public FetchExtensionAction(IJSRuntime? js)
        {
            JavascriptRuntime = js;
        }

    }
}
