using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Stand
{
    [method: JsonConstructor]
    public class Uart : IEquatable<Uart>
    {
        /// <summary>
        /// Uart id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Индекс порта Uart. Может назвать это порт?
        /// </summary>
        public byte Index { get; set; }
        /// <summary>
        /// Полное название
        /// </summary>
        public string Name { get; set; }


        #region Constructors


        public Uart()
        {
            
        }

        public Uart(long id, byte index, string name)
        {
            Id = id;
            Index = index;
            Name = name;
        }



        #endregion Constructors


        public bool Equals(Uart other)
        {
            return Index == other.Index;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Index} {Name}";
        }
    }
}
