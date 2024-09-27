using IotRemoteLab.Blazor.Services;

namespace IotRemoteLab.Blazor.Components
{
    public partial class StreamFrame : Component
    {
        private bool _isStreamActive;
        private bool _isLoadingStream;

        protected override async Task OnAfterRenderAsync(bool firstRender)
		{
            await Init();
			await base.OnAfterRenderAsync(firstRender);
        }

		async Task Init()
        {
            await _janusWebRtcService.InitializeJanus();
            //await Task.Delay(6000);
            //await StartStream();
		}

        async Task StartStream()
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
