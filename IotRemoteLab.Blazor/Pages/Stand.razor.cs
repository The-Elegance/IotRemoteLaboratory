namespace IotRemoteLab.Blazor.Pages
{
    public partial class Stand
    {
        private List<object> UartList = new()
        {
            "UART 1.1","UART 1.2","UART 1.3","UART 1.4"
        };

        // выбор скорость обмена данными (combobox);

        private bool CameraState { get; set; }
        private List<string> TerminalLines { get; set; } = new();
        private uint FontSize = 14;

        void TerminalSendMessage(string command)
        {
            if (command?.Length == 0)
                return;

            // SignalR send message
        }

        void OnButtonStateChanged(Tuple<string, bool> tuple)
        {
            //SignalR send message
            //Publisher.PublishMessageAsync(Topics.LedButtonState.Replace("+", stand.Id.ToString()).Replace("#", tuple.Item1), tuple.Item2 ? 1.ToString() : 0.ToString());
        }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {

                //if (!Publisher.IsConnected)
                //    Publisher.Connect();
                //if (!Subscriber.IsConnected)
                //    Subscriber.Connect();

                //Subscriber.MessageReceivedEvent += (topic, value) =>
                //{
                //    if (topic.Contains("/gpio/led/"))
                //    {
                //        var buttonPort = topic.Replace("/lab/stand/0/gpio/led/", "");
                //        foreach (var buttonRow in stand.McuPlatform.ListRowsButtons)
                //        {
                //            if (buttonRow.First().Title.EndsWith("_") != buttonPort.EndsWith("_"))
                //                continue;

                //            foreach (var button in buttonRow)
                //            {
                //                if (button.Title == buttonPort)
                //                {
                //                    if (value == "1")
                //                        button.IsActive = true;
                //                    else
                //                        button.IsActive = false;
                //                }
                //            }
                //        }
                //    }
                //    else if (topic.Contains("/serial/out"))
                //    {
                //        TerminalLines.Add(value);
                //    }
                //    else if (topic.Contains("/serial/in"))
                //    {
                //        TerminalLines.Add($"[Server]{value}");
                //    }

                //    InvokeAsync(StateHasChanged);
                //};
            }

            base.OnAfterRender(firstRender);
        }


        #region MqttMessage Handlers



        #endregion MqttMessage Handlers
    }
}
