using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Radzen;
using Blazored.LocalStorage;
using Fluxor;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<DialogService>();
builder.Services.AddI18nText();

builder.Services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new[] { "en", "es" };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
    options.AddSupportedCultures(supportedCultures);
    options.AddSupportedUICultures(supportedCultures);    

});

builder.Services.AddFluxor(o =>
{
    o.ScanAssemblies(typeof(Program).Assembly);
    o.UseReduxDevTools(rdt =>
    {
        rdt.Name = "Banco";
    });
});
builder.Services.AddRadzenComponents();
builder.Services.AddBlazoredLocalStorage();

var app=builder.Build();
await app.RunAsync();

