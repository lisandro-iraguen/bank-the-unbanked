using Api.Wallet;
using Api.WebData.Developers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<IWalletData, WalletData>();
        builder.Services.AddSingleton<IWebData, WebDeveloperData>();
    }
}
