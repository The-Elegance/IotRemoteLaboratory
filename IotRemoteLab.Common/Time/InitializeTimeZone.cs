using IotRemoteLab.Common.Provider;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace IotRemoteLab.Common.Time
{
    public sealed class InitializeTimeZone : ComponentBase
    {
        [Inject] public BrowserTimeProvider TimeProvider { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && TimeProvider is BrowserTimeProvider browserTimeProvider && !browserTimeProvider.IsLocalTimeZoneSet)
            {
                try
                {
                    await using var module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/js/timezone.js");
                    var timeZone = await module.InvokeAsync<string>("getBrowserTimeZone");
                    browserTimeProvider.SetBrowserTimeZone(timeZone);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
