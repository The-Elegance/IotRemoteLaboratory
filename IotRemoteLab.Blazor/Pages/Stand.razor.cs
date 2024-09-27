
using IotRemoteLab.Blazor.Components;
using IotRemoteLab.Blazor.Services;
using IotRemoteLab.Blazor.Tools;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace IotRemoteLab.Blazor.Pages
{
    public partial class Stand
    {
        /// <summary>
        /// Id стенда.
        /// </summary>
        [Parameter]
        public long Id { get; set; }

        public Guid SessionId { get => Guid.NewGuid(); }

        private StandService Service;


        private MonacoEditor codeEditor;


        #region Public & Protected Methods


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Service = new StandService(_httpClient, _hubConnection, Id);
            await Service.Init();
            Service.StandStateChanged += Service_StandStateChanged;
            await InvokeAsync(StateHasChanged);

            Service.EnterDeltaDataDelivered += (delta) =>
            {
                codeEditor.Initialize(delta);
            };

            await InvokeAsync(StateHasChanged);
        }


        #endregion Public & Protected Methods


        #region Private Method


        /// <summary>
        /// Отправляет сообщение из консоли на сервер.
        /// </summary>
        /// <param name="command">Команда</param>
        void TerminalSendMessage(string command)
        {
            if (command?.Length == 0)
                return;

            var datetime = DateTime.Now;

            Service.TerminalSendCommand(datetime, SessionId, command);
        }


        
        void OnButtonStateChanged(Tuple<string, bool> tuple)
        {
            Service.ButtonStateChanged(tuple.Item1, tuple.Item2);
        }

        private void Service_StandStateChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        private async void OnCodeChanged(string value)
        {
            ConsoleDebug.WriteLine(value);
            await _hubConnection.SendAsync("CodeUpdate", Id, value);
        }

        private void OnSignalButtonPressed(string portId) 
        {
            Service.ButtonStateChanged(portId, false);
        }

        private void OnSignalButtonUnpressed(string portId) 
        {
            Service.ButtonStateChanged(portId, true);
        }


        #endregion Private Methods
    }
}
