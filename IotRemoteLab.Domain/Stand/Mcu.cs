namespace IotRemoteLab.Domain.Stand
{
    public class Mcu
    {
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
