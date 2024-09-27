using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Domain.Stand.Benchboards
{
    public class BenchboardPort
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Тип порта: ввод, вывод.
        /// </summary>
        public BenchboardPortType Type { get; set; }
        /// <summary>
        /// Порт на Raspberry Pi.
        /// </summary>
        public uint RaspberryPiPort { get; set; }
        /// <summary>
        /// Порт микроконтроллера. (PA_5, 10, 10D, A3 и т.д)
        /// </summary>
        public string McuPort { get; set; }
    }
}
