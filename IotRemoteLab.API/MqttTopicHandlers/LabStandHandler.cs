namespace IotRemoteLab.API.MqttTopicHandlers
{
    public class LabStandHandler
    {
        public event Action<Guid, bool> Led;
        public event Action<Guid, bool> Webcamera;
        // сообщения, полученные от устройства через UART
        public event Action<Guid, string> SerialIn;
        // топик для служебных сообщений о состоянии загрузки ПО на отладочную плату
        public event Action<Guid, string> DebugUpload;
        /// <summary>
        /// Guid - portId
        /// int - subport
        /// bool - value
        /// </summary>
        public event Action<Guid, string, bool> GpioButton;
        public event Action<Guid, string, bool> GpioLed;

        private Dictionary<string, Action<string[]>> ActionByTopic = [];
        private readonly string[] _topicParts;
        private readonly string _topic;
        private readonly string _value;
        public readonly Guid StandId;


        public LabStandHandler(string topic, string value)
        {
            _topic = topic;
            _value = value;
            _topicParts = topic.Replace("/lab/stand/", "").Split("/");
            Guid.TryParse(_topicParts[0], out StandId);

			ActionByTopic["led"] = LedStateChanged;
            ActionByTopic["webcamera"] = WebcameraStateChanged;
            ActionByTopic["serial-in"] = SerialInChanged;
            ActionByTopic["debug-upload"] = DebugUploadChanged;
            ActionByTopic["gpio-input"] = GpioLedChanged;
            ActionByTopic["gpio-output"] = GpioButtonChanged;
        }

        public void Execute() 
        {
            switch (_topicParts.Length) 
            {
                case 2:
                    {
                        // led or webcamera
                        ActionByTopic[_topicParts[1]]([_topicParts[2]]);
                    }
                    break;
                case 3:
                    {
                        if (ActionByTopic.TryGetValue($"{_topicParts[1]}-{_topicParts[2]}", out var action))
                        {
                            action([_value]);
                        }
                        else 
                        {
                            UnknownTokenError();
                        }
                    } 
                    break;
                case 4: 
                    {
                        if (ActionByTopic.TryGetValue($"{_topicParts[1]}-{_topicParts[2]}", out var action))
                        {
                            action([_topicParts[3], _value]);
                        }
                        else
                        {
                            UnknownTokenError();
                        }
                    } 
                    break;
            }
        }

        public void LedStateChanged(string[] values) 
        {
            Led?.Invoke(StandId, int.Parse(values[0]) > 0);
        }

        public void WebcameraStateChanged(string[] values)
        {
            Webcamera?.Invoke(StandId, int.Parse(values[0]) > 0);
        }

        private void SerialInChanged(string[] values)
        {
            SerialIn?.Invoke(StandId, values[0]);
        }

        private void DebugUploadChanged(string[] values)
        {
            DebugUpload?.Invoke(StandId, values[0]);
        }

        private void GpioLedChanged(string[] values) 
        {
            GpioLed?.Invoke(StandId, values[0], int.Parse(values[1]) > 0);
        }

        private void GpioButtonChanged(string[] values) 
        {
            GpioButton?.Invoke(StandId, values[1], int.Parse(values[2]) > 0);
        }

        private void UnknownTokenError() 
        {
            throw new Exception($"Нет известных операций для данного топика {string.Join("/", _topicParts)}");
        }
    }
}
