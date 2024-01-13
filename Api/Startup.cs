using Api.Wallet;
using Api.WebData.Developers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using CardanoSharp.Koios.Client;
using Api.Services.Transaction;
using Api.Services.Policy;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddKoios("https://preview.koios.rest/api/v0");

        builder.Services.AddSingleton<IWalletData, WalletData>();
        builder.Services.AddSingleton<IWebData, WebDeveloperData>();
        builder.Services.AddSingleton<IPolicyManager, PolicyManager>();
        builder.Services.AddSingleton<ITransactionService, TransactionService>();

    }
}
