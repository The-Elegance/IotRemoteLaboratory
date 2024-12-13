using Microsoft.JSInterop;
using System.Text.Json;

namespace IotRemoteLab.Common.Services.LocalStorage
{
    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;


        public LocalStorageService(IJSRuntime jSRuntime)
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
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
        }

        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }


        #endregion Public Methods
    }
}
