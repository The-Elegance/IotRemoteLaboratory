using Microsoft.AspNetCore.SignalR;

namespace IotRemoteLab.API.Hubs
{
    public class StandHub : Hub
    {
        public async void Init(string test) 
        {
            Console.WriteLine(test);
        }

        /*
         * Серверные методы - предназначены для вызова сервером.
         * SendCodeExecuteResult - отправляет результат запуска кода, всем пользователям в комнате.
         * SendTerminalLog - отправляет новый лог в терминал.
         * 
         * **/


        public async Task SendCodeExecuteResult(Guid standId, Guid editorElementId, string result) 
        {
            await Clients.All.SendAsync("CodeExecuteResultChanged", standId, editorElementId, result);
        }


        /*
         * Клиентские методы - предназначены для вызова клиентом.
         * 
         * **/

        public async Task SendToTopic(string topic, string args) 
        {
            await Clients.All.SendAsync("TopicValueChanged", topic, args);
        }

        public async Task CodeUpdate(string newValue) 
        {
            await Clients.Others.SendAsync("OnCodeUpdated", newValue);
        }

        public async Task SelectUart(Guid id) 
        {
            await Clients.Others.SendAsync("UartTypeChanged", id);
        }

        /// <summary>
        /// Raspberry Pi эмулирует нажатие кнопки (подаёт сигнал 0 или 1 на контакт)
        /// </summary>
        /// <returns></returns>
        public async Task RaspberryPiOutPort(byte signalValue) 
        {
            await Clients.All.SendAsync("RaspberryPiOutPortChanged", signalValue);
        }

        /// <summary>
        /// Raspberry Pi работает в режиме отслеживания состояния контакта (подаёт сигнал 0 или 1 на контакт)
        /// </summary>
        /// <returns></returns>
        public async Task RaspberryPiInPort(Guid guid, byte signalValue)
        {
            await Clients.All.SendAsync("RaspberryPiInPortChanged", guid, signalValue);
        }
    }
}
