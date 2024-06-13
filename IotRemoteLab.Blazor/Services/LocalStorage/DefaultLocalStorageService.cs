
using Microsoft.JSInterop;
using System.Text.Json;

namespace IotRemoteLab.Blazor.Services.LocalStorage
{
	public class DefaultLocalStorageService : ILocalStorageService
	{
		private IJSRuntime _jsRuntime;


		public DefaultLocalStorageService(IJSRuntime jSRuntime)
		{
			_jsRuntime = jSRuntime;
		}


		#region Public Methods


		public async Task<T> GetItemAsync<T>(string key)
		{
			var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);

			if (json == null) 
			{
				return default;
			}

			return JsonSerializer.Deserialize<T>(json);
		}

		public async Task SetItemAsync<T>(string key, T value)
		{
			await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize<T>(value));
		}

		public async Task RemoveItemAsync(string key)
		{
			await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
		}


		#endregion Public Methods
	}
}
