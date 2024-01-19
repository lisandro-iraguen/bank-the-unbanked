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

[assembly: FunctionsStartup(typeof(Api.Startup))]

namespace Api;



public class Startup : FunctionsStartup
{

    public override void Configure(IFunctionsHostBuilder builder)
    {
        string dir = Environment.CurrentDirectory;
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(dir)
            .AddJsonFile("application.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();


        builder.Services.AddKoios("https://preview.koios.rest/api/v0");
        builder.Services.AddSingleton<IWalletData, WalletData>();
        builder.Services.AddSingleton<IWebData, WebDeveloperData>();
        builder.Services.AddSingleton<IPolicyManager>(x =>
            new PolicyManager(config));
        builder.Services.AddSingleton<ITransactionService, TransactionService>();

      

        
        

    }
}
