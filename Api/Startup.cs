using Api.Wallet;
using Api.WebData.Developers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using CardanoSharp.Koios.Client;
using Api.Services.Transaction;
using Api.Services.Policy;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using Api.Services.Oracle;

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api;



public class Startup : FunctionsStartup
{

    public override void Configure(IFunctionsHostBuilder builder)
    {

        var configuration = BuildConfiguration(builder.GetContext().ApplicationRootPath);
        builder.Services.AddSingleton<IConfiguration>(configuration);

        var koiosURL = configuration["KoiosURL"];
        builder.Services.AddKoios(configuration["KoiosURL"]);
        builder.Services.AddSingleton<IWalletData, WalletData>();
        builder.Services.AddSingleton<IWebData, WebDeveloperData>();
        builder.Services.AddSingleton<IPriceServices, PriceServices>();
        builder.Services.AddSingleton<IPolicyManager, PolicyManager>();
        builder.Services.AddSingleton<ITransactionService,TransactionService>();

    }

    private IConfiguration BuildConfiguration(string applicationRootPath)
    {
        var config =
            new ConfigurationBuilder()
                .SetBasePath(applicationRootPath)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

        return config;
    }
}
