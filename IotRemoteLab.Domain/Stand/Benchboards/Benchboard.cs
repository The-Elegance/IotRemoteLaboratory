namespace IotRemoteLab.Domain.Stand.Benchboards
{
    public class Benchboard
    {
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
