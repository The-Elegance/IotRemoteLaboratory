using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Common.Time
{
    public sealed class LocalTime : ComponentBase, IDisposable
    {
        [Inject]
        public BrowserTimeProvider TimeProvider { get; set; } = default!;

        [Parameter]
        [EditorRequired]
        public DateTime? DateTime { get; set; }

        [Parameter]
        public string? Format { get; set; }

        protected override void OnInitialized()
        {
            if (TimeProvider is BrowserTimeProvider browserTimeProvider)
            {
                browserTimeProvider.LocalTimeZoneChanged += LocalTimeZoneChanged;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (DateTime != null)
            {
                if (string.IsNullOrWhiteSpace(Format))
                {
                    builder.AddContent(0, TimeProvider.ToLocalDateTime(DateTime.Value));
                }
                else
                {
                    builder.AddContent(0, TimeProvider.ToLocalDateTime(DateTime.Value).ToString(Format));
                }
            }
        }

        public void Dispose()
        {
            if (TimeProvider is BrowserTimeProvider browserTimeProvider)
            {
                browserTimeProvider.LocalTimeZoneChanged -= LocalTimeZoneChanged;
            }
        }

        private void LocalTimeZoneChanged(object? sender, EventArgs e)
        {
            _ = InvokeAsync(StateHasChanged);
        }
    }

    public static class TimeProviderExtensions
    {
        public static DateTime ToLocalDateTime(this TimeProvider timeProvider, DateTime dateTime)
        {
            return dateTime.Kind switch
            {
                DateTimeKind.Unspecified => throw new InvalidOperationException("Unable to convert unspecified DateTime to local time"),
                DateTimeKind.Local => dateTime,
                _ => DateTime.SpecifyKind(TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeProvider.LocalTimeZone), DateTimeKind.Local),
            };
        }

        public static DateTime ToLocalDateTime(this TimeProvider timeProvider, DateTimeOffset dateTime)
        {
            var local = TimeZoneInfo.ConvertTimeFromUtc(dateTime.UtcDateTime, timeProvider.LocalTimeZone);
            local = DateTime.SpecifyKind(local, DateTimeKind.Local);
            return local;
        }
    }
}