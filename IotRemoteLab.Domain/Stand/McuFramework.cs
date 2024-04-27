namespace IotRemoteLab.Domain.Stand
{
    public class McuFramework
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Название фреймворка.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Стартовый паттерн фреймворка.
        /// </summary>
        public string Pattern { get; set; }

        // TODO: Заменить string Pattern на Boilerplate Code
    }
}
