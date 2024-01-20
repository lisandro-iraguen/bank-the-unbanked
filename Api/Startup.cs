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

        string configPath = Path.Combine(Environment.CurrentDirectory, "local.settings.json");

        // Create a ConfigurationBuilder and load settings from local.settings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configPath, optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

     
       

        builder.Services.AddKoios("https://preview.koios.rest/api/v0");
        builder.Services.AddSingleton<IWalletData, WalletData>();
        builder.Services.AddSingleton<IWebData, WebDeveloperData>();
        builder.Services.AddSingleton<IPolicyManager>(x =>
            new PolicyManager(configuration));
        builder.Services.AddSingleton<ITransactionService, TransactionService>();

      

        
        

    }
}
