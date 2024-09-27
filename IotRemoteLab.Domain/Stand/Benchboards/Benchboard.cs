using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Domain.Stand.Benchboards
{
    /// <summary>
    /// Стендовая плата
    /// </summary>
    public class Benchboard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Название стендовой платы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Порты стендовой платы
        /// </summary>
        public List<BenchboardPort> Ports { get; set; }
    }
}
