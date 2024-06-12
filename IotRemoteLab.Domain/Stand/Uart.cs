using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Stand
{
    [method: JsonConstructor]
    public class Uart : IEquatable<Uart>
    {
        public Uart()
        {
            
        }
        
        public Uart(Guid id, byte index, string name)
        {
            Id = id;
            Index = index;
            Name = name;
        }
        
        
        /// <summary>
        /// Uart id.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Индекс порта Uart. Может назвать это порт?
        /// </summary>
        public byte Index { get; set; }
        /// <summary>
        /// Полное название
        /// </summary>
        public string Name { get; set; }

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
