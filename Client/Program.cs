using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Radzen;
using Microsoft.Extensions.Configuration;
using Client.Pages;
using Utils;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");




builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DialogService>();


builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();


await builder.Build().RunAsync();
