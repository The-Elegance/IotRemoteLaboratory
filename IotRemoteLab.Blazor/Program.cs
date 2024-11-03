//using Blazored.LocalStorage;
using AntDesign;
using IotRemoteLab.Blazor;
using IotRemoteLab.Blazor.Providers;
using IotRemoteLab.Blazor.Services;
using IotRemoteLab.Blazor.Services.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAntDesign();

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri("https://localhost:7216")
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();
builder.Services.AddSingleton<ILocalStorageService, DefaultLocalStorageService>();
//builder.Services.AddBlazoredLocalStorageAsSingleton();

builder.Services.AddScoped(sp => {
	var navigationManager = sp.GetRequiredService<NavigationManager>();
	return new HubConnectionBuilder()
	  .WithUrl("https://localhost:7216/stand-hub")
	  .WithAutomaticReconnect()
	  .Build();
});

builder.Services.AddScoped<MonacoEditorService>();
builder.Services.AddSingleton<JanusWebRtcService>();

await builder.Build().RunAsync();