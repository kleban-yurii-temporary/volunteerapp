using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VolunteerRequestApp.Client;
using VolunteerRequestApp.Client.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<HttpCurrencyPairService>();
builder.Services.AddScoped<HttpRequestService>();

await builder.Build().RunAsync();
