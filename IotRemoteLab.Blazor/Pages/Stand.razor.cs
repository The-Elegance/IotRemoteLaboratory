
using IotRemoteLab.Blazor.Components;
using IotRemoteLab.Blazor.Services;
using IotRemoteLab.Blazor.Tools;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace IotRemoteLab.Blazor.Pages
{
    public partial class Stand
    {
        /// <summary>
        /// Id стенда.
        /// </summary>
        [Parameter]
        public Guid Id { get; set; }

        private StandService Service;


        /// <summary>
        /// Имеет ли стенд камеру.
        /// </summary>
        private bool HasCamera;
        /// <summary>
        /// Включена ли веб-трансляция.
        /// </summary>
        private bool CameraEnableState;
        /// <summary>
        /// Ссылка на трансляцию.
        /// </summary>
        private Uri CameraUri;

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

            // SignalR send message
            Service.TerminalLogs.Add(command);
        }

        private async void OnCodeChanged(string value)
        {
            ConsoleDebug.WriteLine(value);
            await _hubConnection.SendAsync("CodeUpdate", Id, value);
        }
        
        async void OnButtonStateChanged(Tuple<string, bool> tuple)
        {
            //SignalR send message
            //Publisher.PublishMessageAsync(Topics.LedButtonState.Replace("+", stand.Id.ToString()).Replace("#", tuple.Item1), tuple.Item2 ? 1.ToString() : 0.ToString());
            //await _hubConnection.SendAsync("SendToTopic", "ButtonStateChanged", tuple.Item1);
        }

        private void Service_StandStateChanged()
        {
            InvokeAsync(StateHasChanged);
        }


        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            return base.OnAfterRenderAsync(firstRender);
        }

        #endregion Private Methods
    }
}
