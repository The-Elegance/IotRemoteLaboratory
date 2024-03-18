namespace IotRemoteLab.Domain.Stand
{
    public class Mcu
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Название микроконтроллера.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Используемый фремворк.
        /// </summary>
        public McuFramework Framework { get; set; }
        /// <summary>
        /// Скрипт сборки - ссылка на файл
        /// </summary>
        public string AssemblyScriptFile { get; set; }
        /// <summary>
        /// Скрипт развертывания ПО на стенде.
        /// </summary>
        public string DeployScriptFile { get; set; }
    }
}
