
using IotRemoteLab.Blazor.Services;
using IotRemoteLab.Domain.Code;
using IotRemoteLab.Domain.Stand;
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
        /// Стандратный Boilderplace Code для прошивки.
        /// </summary>
        private BoilerplateCode _defaultBoilderplateCode;


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

        /// <summary>
        /// Данные в терминала
        /// </summary>
        private List<string> TerminalLines { get; set; } = [];


        #region Public & Protected Methods


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Service = new StandService(_httpClient, _hubConnection, Id);
            await Service.Init();
            Service.StandStateChanged += Service_StandStateChanged;
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
            //TerminalLines.Add(command);
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


        #endregion Private Methods


        #region Api Methods


        private async Task<T> GetDataFromApi<T>(string toAddr)
        {
            return await GetDataFromApi<T>(toAddr, default);
        }

        private async Task<T> GetDataFromApi<T>(string toAddr, T defaultParam)
        {
            var s = await _httpClient.GetFromJsonAsync<T>(toAddr);
            return s ?? defaultParam;
        }


        #endregion
    }
}
