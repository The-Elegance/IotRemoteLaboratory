﻿using IotRemoteLab.Domain.Stand.Benchboards;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Stand
{
    public class Stand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; init; }
        /// <summary>
        /// Микроконтроллер.
        /// </summary>
        public Mcu Mcu { get; set; }
        /// <summary>
        /// Фреймворк микроконтроллера.
        /// </summary>
        [JsonIgnore]
        public McuFramework Framework { get => Mcu.Framework; }
        /// <summary>
        /// Url (Доменное имя)
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Наличие стендовой платы
        /// </summary>
        public bool HasBenchboard { get; set; }
        /// <summary>
        /// Стендовая плата, если HasBenchboard = false, то значением будет null.
        /// </summary>
        public Benchboard? Benchboard { get; set; }
        /// <summary>
        /// Наличие подсветки стенда.
        /// </summary>
        public bool HasLighting { get; set; }
        /// <summary>
        /// Яркость подсветки, значения 0-100.
        /// </summary>
        public uint LightingBrightnessLevel { get; set; }
        /// <summary>
        /// Порт подсветки на Raspberry Pi. 
        /// </summary>
        public uint LightingRaspberryPiPort { get; set; }
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
        /// <summary>
        /// Доступные Uarts для стенда.
        /// </summary>
        public List<Uart> AvailableUarts { get; set; }
        /// <summary>
        /// Id для редактора кода
        /// </summary>
        public Guid CodeEditorId { get; set; }
    }
}
