using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using IotRemoteLab.Blazor.Services;
using Microsoft.AspNetCore.SignalR.Client;
using IotRemoteLab.Domain.Code;

namespace IotRemoteLab.Blazor.Components
{
    public partial class MonacoEditor
    {
        private BoilerplateCode _defaultBoilerplateCode = new BoilerplateCode("cpp", "17", string.Empty);
        private bool _isReadonly = false;
        private uint _currentFontSize = 14;
        private string _output = string.Empty;

        [Parameter]
        public uint CurrentFontSize
        {
            get => _currentFontSize; set
            {
                _currentFontSize = value;
                ChangedFontSize();
            }
        }

        [Parameter]
        public bool IsReadonly
        {
            get => _isReadonly; set
            {
                _isReadonly = value;
                ChangeReadonlyMode();
            }
        }

        [Parameter]
        public BoilerplateCode DefaultBoilerplateCode
        {
            get => _defaultBoilerplateCode; set
            {
                _defaultBoilerplateCode = value;
                try
                {
                    if (Service.GetCode("container").Result == string.Empty)
                    {
                        Service.SetCode("container", value.Value);
                    }
                }
                catch { }
            }
        }
    }

    public partial class MonacoEditor : Component
    {
        private int _codeVersion;
        private int _otherCodeVersion;

        #region Properties


        [Inject]
        public MonacoEditorService Service { get; set; }
        [Inject]
        public HttpClient HttpClient { get; set; }


        /// <summary>
        /// Выполняет код, string - code, action - выполняется при завершении работы метода.
        /// </summary>
        [Parameter]
        public Action<string, Action<string>> ExecuteCode { get; set; }

        [Parameter]
        public string DebugUpload { get; set; }

        #endregion Properties


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
            //var browserFile = e.File;

            //if (browserFile == null)
            //    return;

            //var fileSize = browserFile.Size;
            //var fileType = browserFile.ContentType;
            //var fileName = browserFile.Name;
            //var lastModified = browserFile.LastModified;

            //try
            //{
            //    var fileStream = browserFile.OpenReadStream(MaxFileSize);

            //    var tempFileName = Path.GetTempFileName();
            //    var extension = Path.GetExtension(fileName);
            //    var targetFilePath = Path.ChangeExtension(tempFileName, extension);

            //    // save temp file
            //    var targetStream = new FileStream(targetFilePath, FileMode.Create);
            //    // copy to target file
            //    await fileStream.CopyToAsync(targetStream);
            //    targetStream.Close();

            //    // TODO: Code executor
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
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
                // TODO: !!! IMPORTANT !!! оставляю пока синхронизацию ide, но решение имеет кучу проблем.
                // В будущем найти какое-нибудь иное решение, ибо доработать данный подход.
                Service.Initialize("container", DefaultBoilerplateCode.Value, DefaultBoilerplateCode.LanguageName, OnIdeCodeChanged);
                _hubConnection.On<string>("OnCodeUpdated", OnCodeUpdated);
            }
        }

        private async void OnIdeCodeChanged(string s)
        {
            _codeVersion = s.GetHashCode();
            if (_codeVersion != _otherCodeVersion)
            {
                await _hubConnection.SendAsync("CodeUpdate", s);
            }
        }

        private void OnCodeUpdated(string obj)
        {
            _otherCodeVersion = obj.GetHashCode();
            Service.SetCode("container", obj);
        }


        #endregion Component Methods
    }
}
