using IotRemoteLab.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using IotRemoteLab.Blazor.Pages;
using Radzen;
using System;
using IotRemoteLab.Domain.Stand;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7216/") });
builder.Services.AddScoped<DialogService>();


builder.Services.AddSingleton(sp => {
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl("https://localhost:7216/stand-hub")
      .WithAutomaticReconnect()
      .Build();
});

builder.Services.AddRadzenComponents();

await builder.Build().RunAsync();
