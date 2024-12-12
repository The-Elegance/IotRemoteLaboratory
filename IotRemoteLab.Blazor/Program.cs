//using Blazored.LocalStorage;
using AntDesign;
using IotRemoteLab.Blazor;
using IotRemoteLab.Blazor.Providers;
using IotRemoteLab.Blazor.Services;
using IotRemoteLab.Common.Services.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAntDesign();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7216/api/v1/")
});

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<UserContext>();
//builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl("https://localhost:7216/stand-hub")
      .WithAutomaticReconnect()
      .Build();
});

builder.Services.AddScoped<MonacoEditorService>();
builder.Services.AddSingleton<JanusWebRtcService>();

var app = builder.Build().RunAsync();