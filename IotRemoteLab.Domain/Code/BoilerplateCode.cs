using System.Text.Json.Serialization;

namespace IotRemoteLab.Domain.Code
{
    [method: JsonConstructor]
    public readonly struct BoilerplateCode(string languageName, string languageVersion, string value)
    {
        /// <summary>
        /// Название языка
        /// </summary>
        public string LanguageName { get; } = languageName;
        /// <summary>
        /// Версия языка
        /// </summary>
        public string LanguageVersion { get; } = languageVersion;
        /// <summary>
        /// Шаблонный код.
        /// </summary>
        public string Value { get; } = value;
    }
}
