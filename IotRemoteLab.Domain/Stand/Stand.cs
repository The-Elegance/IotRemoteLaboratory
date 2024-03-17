using IotRemoteLab.Domain.Stand.Benchboards;

namespace IotRemoteLab.Domain.Stand
{
    public class Stand
    {
        /// <summary>
        /// Микроконтроллер.
        /// </summary>
        public Mcu Mcu { get; set; }
        /// <summary>
        /// Фреймворк микроконтроллера.
        /// </summary>
        public McuFramework Framework { get => Mcu.Framework; }
        /// <summary>
        /// Наличие стендовой платы
        /// </summary>
        public bool HasBenchboard { get; set; }
        /// <summary>
        /// Стендовая плата, если HasBenchboard = false, то значением будет null.
        /// </summary>
        public Benchboard Benchboard { get; set; }
        /// <summary>
        /// Наличие подсветки стенда.
        /// </summary>
        public bool HasLighting { get; set; }
        /// <summary>
        /// Яркость подсветки, значения 0-100.
        /// </summary>
        public uint LigthingBrightnessLevel { get; set; }
        /// <summary>
        /// Порт подсветки на Raspberry Pi. 
        /// </summary>
        public uint LigthingRaspberryPiPort { get; set; }
        /// <summary>
        /// Наличие последовательного порта.
        /// </summary>
        public bool HasSerialPort { get; set; }
        /// <summary>
        /// Скорость последовательного порта.
        /// </summary>
        public uint SerialPortSpeed { get; set; }
        /// <summary>
        /// Наличие вебкамеры.
        /// </summary>
        public bool HasWebcam { get; set; }
        /// <summary>
        /// Ссылка на web трансляцию.
        /// </summary>
        public string WebcamUrl { get; set;}
    }
}
