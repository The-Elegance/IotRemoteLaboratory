using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;

namespace IotRemoteLab.Blazor.Components
{
    public partial class MonacoEditor : Component
    {
        const int MaxFileSize = 5000 * 1024;
        private string _output = string.Empty;


        #region Properties


        /// <summary>
        /// Выполняет код, string - code, action - выполняется при завершении работы метода.
        /// </summary>
        [Parameter]
        public Action<string, Action> ExecuteCode { get; set; }

        private uint _currentFontSize = 14;
        [Parameter]
        public uint CurrentFontSize
        {
            get => _currentFontSize; set
            {
                _currentFontSize = value;
                ChangedFontSize();
            }
        }

        private bool _isReadonly = false;
        [Parameter]
        public bool IsReadonly
        {
            get => _isReadonly; set
            {
                _isReadonly = value;
                ChangeReadonlyMode();
            }
        }


        /// <summary>
        /// Сохраняет код с ide и запускает его.
        /// </summary>
        public async void RunClick()
        {

        }

        /// <summary>
        /// Загружает пользовательский файл на компьютер
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task FileUploaded(InputFileChangeEventArgs e)
        {

        }


        #endregion Properties


        #region Private Methods


        private void ChangeReadonlyMode()
        {
            
        }

        public void ChangedFontSize()
        {

        }


        #region Button Clicks


        public void DecreaseFontSize()
        {
            CurrentFontSize--;
        }

        public void IncreaseFontSize()
        {
            CurrentFontSize++;
        }


        #endregion Button Clicks


        #endregion Monaco Editor Methods


        #region Component Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }


        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }


        #endregion Component Methods
    }
}
