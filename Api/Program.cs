using Api;
using Api.Services.Developers;
using Api.Services.Oracle;
using Api.Services.Policy;
using Api.Services.Transaction;
using Api.Services.Wallet;
using CardanoSharp.Koios.Client;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{


    public static async Task Main(string[] args)
    {
        var builder = Host
          .CreateDefaultBuilder(args)
          .ConfigureFunctionsWorkerDefaults()
          .ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();
           })
         .ConfigureServices((hostContext, builder) =>
         {
             var configuration = (IConfigurationRoot) hostContext.Configuration;
             var localSettingsJsonProvider = configuration.Providers.LastOrDefault(provider => provider.GetType() == typeof(JsonConfigurationProvider));
             if (localSettingsJsonProvider != null)
             {
                 var koiosURL = configuration["KoiosURL"];
                 builder.AddKoios(configuration["KoiosURL"]);
             }

         
             builder.AddSingleton<IWalletData, WalletData>();
             builder.AddSingleton<IWebData, WebDeveloperData>();
             builder.AddSingleton<IPriceServices, PriceServices>();
             builder.AddSingleton<IPolicyManager, PolicyManager>();
             builder.AddSingleton<ITransactionService, TransactionService>();

         });

        await builder.Build().RunAsync();
    }
   
}