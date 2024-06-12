using Blazored.LocalStorage;
using IotRemoteLab.Blazor;
using IotRemoteLab.Blazor.Providers;
using IotRemoteLab.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5229/") });

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddSingleton<IAccessTokenProvider, AccessTokenProvider>();


builder.Services.AddSingleton(sp => {
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    var tokenProvider = sp.GetService<IAccessTokenProvider>()!;
    return new HubConnectionBuilder()
      .WithUrl("https://localhost:7216/stand-hub", 
          options => options.AccessTokenProvider = () => Task.FromResult($"bearer {tokenProvider.GetToken()}") )
      
      .WithAutomaticReconnect()
      .Build();
});

builder.Services.AddScoped<MonacoEditorService>();

builder.Services.AddScoped<MonacoEditorService>();

await builder.Build().RunAsync();
