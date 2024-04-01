using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using IotRemoteLab.Blazor.Services;

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
        public Action<string, Action<string>> ExecuteCode { get; set; }

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

        [Inject]
        public MonacoEditorService Service { get; set; }
        [Inject]
        public HttpClient HttpClient { get; set; }


        #endregion Properties


        #region Constructors


        public MonacoEditor() { }


        #endregion Constructors


        #region Private Methods


        /// <summary>
        /// Отправляет код из ide на сервер, выводит результат сборки.
        /// </summary>
        public async void RunClick()
        {
            IsReadonly = !IsReadonly;
            _output = string.Empty;

            var latestCode = Service.GetCode("container").Result;
            ExecuteCode?.Invoke(latestCode, (res) => 
            {
                _output = res;
            });
        }

        /// <summary>
        /// Загружает пользовательский файл на компьютер
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task FileUploaded(InputFileChangeEventArgs e)
        {
            var browserFile = e.File;

            if (browserFile == null)
                return;

            var fileSize = browserFile.Size;
            var fileType = browserFile.ContentType;
            var fileName = browserFile.Name;
            var lastModified = browserFile.LastModified;

            try
            {
                var fileStream = browserFile.OpenReadStream(MaxFileSize);

                var tempFileName = Path.GetTempFileName();
                var extension = Path.GetExtension(fileName);
                var targetFilePath = Path.ChangeExtension(tempFileName, extension);

                // save temp file
                var targetStream = new FileStream(targetFilePath, FileMode.Create);
                // copy to target file
                await fileStream.CopyToAsync(targetStream);
                targetStream.Close();

                // TODO: Code executor
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void ChangeReadonlyMode()
        {
            Service.ReadonlyMode("container", IsReadonly);
        }

        public void ChangedFontSize()
        {
            Service.ChangeFontSize("container", CurrentFontSize);
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
            Service.Reload("container");
            if (firstRender) 
            {
                Service.Initialize("container", @"void setup() { 
                    Serial.begin(9600); //initialize serial communication
                    pinMode(ledPin, OUTPUT); //define ledPin as an output
                }", "cpp", (s) => { });
            }
        }


        #endregion Component Methods
    }
}
