using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using POCA.Web;
using POCA.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddTransient<AlunoAPI>();
builder.Services.AddTransient<MateriaAPI>();
builder.Services.AddTransient<ProfessorAPI>();
builder.Services.AddTransient<QuestoesAPI>();

builder.Services.AddHttpClient("API", client => {
	client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
	client.DefaultRequestHeaders.Add("Accept", "application/json");
});
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
    config.SnackbarConfiguration.HideTransitionDuration = 200;
    config.SnackbarConfiguration.ShowTransitionDuration = 200;
    config.SnackbarConfiguration.VisibleStateDuration = 3000;
    config.SnackbarConfiguration.MaxDisplayedSnackbars = 3;
});
