using IotRemoteLab.Blazor.Services;

namespace IotRemoteLab.Blazor.Components
{
    public partial class StreamFrame : Component
    {
        protected override Task OnAfterRenderAsync(bool firstRender)
		{
            if (firstRender)
            {
                Init();
            }

			return base.OnAfterRenderAsync(firstRender);
		}

		async void Init()
        {
            await _janusWebRtcService.InitializeJanus();
		}

        async void StartStream()
        {
            await _janusWebRtcService.StartVideoStreaming();
        }

        async void StopStream()
        {
            await _janusWebRtcService.StopVideoStreaming();
        }
    }
}
