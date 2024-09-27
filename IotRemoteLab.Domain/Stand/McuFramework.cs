using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Domain.Stand
{
    public class McuFramework
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        /// <summary>
        /// Название фреймворка.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Название файла с кодом.
        /// </summary>
        public string CodeFileName { get; set; }
        /// <summary>
        /// Расширение файла [.cpp, ]
        /// </summary>
        public string CodeFileExtension { get; set; }
        /// <summary>
        /// Стартовый паттерн фреймворка.
        /// </summary>
        public string Pattern { get; set; }

        // TODO: Заменить string Pattern на Boilerplate Code
    }
}
