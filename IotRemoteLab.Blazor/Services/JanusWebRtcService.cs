using Microsoft.JSInterop;

namespace IotRemoteLab.Blazor.Services
{
    public class JanusWebRtcService
    {
		private readonly IJSRuntime _jsRuntime;


		#region Constructors


		public JanusWebRtcService(IJSRuntime jSRuntime)
		{
			_jsRuntime = jSRuntime;
		}


		#endregion Constructor


		#region Public Methods


		public async Task InitializeJanus()
		{
			await _jsRuntime.InvokeVoidAsync("startJanusStreamModule");
		}

		public async Task StartVideoStreaming()
		{
			await _jsRuntime.InvokeVoidAsync("startStream", 10);
		}

		public async Task StopVideoStreaming()
		{
			await _jsRuntime.InvokeVoidAsync("stopStream");
		}


		#endregion Public Methods
	}
}
