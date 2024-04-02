using Microsoft.JSInterop;

namespace IotRemoteLab.Blazor.Services
{
    public class MonacoEditorService
    {
        private readonly IJSRuntime _runtime;
        private Action<string>? _codeChanged;

        public MonacoEditorService(IJSRuntime runtime)
        {
            _runtime = runtime;   
        }

        #region Public Methods

        public void Initialize(string elementId, string initialCode, string language, Action<string> codeChanged)
        {
            _codeChanged = codeChanged;
            _runtime.InvokeVoidAsync("monacoInterop.initialize", elementId, initialCode, language, DotNetObjectReference.Create(this));
        }

        public void Reload(string elementId)
        {
            _runtime.InvokeVoidAsync("monacoInterop.reload", elementId);
        }

        public async ValueTask<string> GetCode(string elementId)
        {
            return await _runtime.InvokeAsync<string>("monacoInterop.getCode", elementId);
        }

        public void SetCode(string elementId, string code)
        {
            _runtime.InvokeAsync<object>("monacoInterop.setCode", elementId, code);
        }

        public void ReadonlyMode(string elementId, bool state)
        {
            _runtime.InvokeVoidAsync("monacoInterop.readonlyMode", elementId, state);
        }

        public void ChangeFontSize(string elementId, uint fontSize)
        {
            _runtime.InvokeVoidAsync("monacoInterop.changeFontSize", elementId, fontSize);
        }

        [JSInvokable]
        public void OnCodeChanged(string newValue) 
        {
            _codeChanged?.Invoke(newValue);
        }


        #endregion Public Methods
    }
}
