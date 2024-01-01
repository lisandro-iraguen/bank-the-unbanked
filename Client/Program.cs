using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Radzen;
using Microsoft.Extensions.Configuration;
using Client.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Configuration
       .AddJsonFile("./appsettings.json") // Ensure proper path if necessary
       .AddJsonFile($"./appsettings.{builder.HostEnvironment.Environment}.json", optional: true);


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DialogService>();

builder.Services.AddRadzenComponents();



await builder.Build().RunAsync();
