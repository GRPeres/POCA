using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using POCA.Web;
using POCA.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddTransient<QuestoesAPI>();
//builder.Services.AddScoped<ArtistaAPI>();

builder.Services.AddHttpClient("API", client => {
	client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
	client.DefaultRequestHeaders.Add("Accept", "application/json");
});
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
