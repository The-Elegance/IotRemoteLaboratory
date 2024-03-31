using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Blazor.Components
{
    public partial class Terminal : Component
    {
        /// <summary>
        /// Текст вводимый пользователем в консоль.
        /// </summary>
        private string _inputStr = string.Empty;


        #region Properties


        /// <summary>
        /// Делегат отправляющий сообщение пользователя.
        /// </summary>
        [Parameter]
        public Action<string>? SendMessageAction { get; set; }
        /// <summary>
        /// Вывод консоли.
        /// </summary>
        [Parameter]
        public List<string> Logs { get; set; } = [];


        #endregion Properties


        #region Private Methods


        /// <summary>
        /// Отправляет сообщение пользователя.
        /// </summary>
        void SendMessage()
        {
            SendMessageAction?.Invoke(_inputStr);
            _inputStr = string.Empty;
            InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Обработка нажатия клавиши Enter.
        /// </summary>
        /// <param name="e"></param>
        void MouseDownHandler(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                SendMessage();
            }
        }


        #endregion Private Methods
    }
}
