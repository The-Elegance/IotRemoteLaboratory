using IotRemoteLab.Blazor.Services;

namespace IotRemoteLab.Blazor.Components
{
    public partial class StreamFrame : Component
    {
        private bool _isStreamActive;

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
            _isStreamActive = true;
        }

        async void StopStream()
        {
            await _janusWebRtcService.StopVideoStreaming();
            _isStreamActive = false;
        }
    }
}
