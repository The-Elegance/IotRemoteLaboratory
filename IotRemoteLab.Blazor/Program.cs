using IotRemoteLab.Blazor;
using IotRemoteLab.Blazor.CLI;
using IotRemoteLab.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<CommandRegistry>();
builder.Services.AddSingleton<CommandExecutor>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7216") });

builder.Services.AddSingleton(sp => {
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl("https://localhost:7216/stand-hub")
      .WithAutomaticReconnect()
      .Build();
});

builder.Services.AddScoped<MonacoEditorService>();

await builder.Build().RunAsync();
