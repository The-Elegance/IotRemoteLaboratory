using AntDesign.Internal;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace IotRemoteLab.Blazor.Models
{
    public class RegistrationUserData
    {
        [Required(ErrorMessage = "Поле Email обязательно к заполнению"), EmailAddress(ErrorMessage = "Неверный формат Email"), MinLength(2, ErrorMessage = "Длина электронного почты должна быть не менее двух символов.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле \"Пароль\" обязательно к заполнению"), StringLength(6, ErrorMessage = "Минимальная длина пароля 6 символа")]
        public string Password { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// Университет
        /// </summary>
        public string University { get; set; }
        /// <summary>
        /// Учебная группа
        /// </summary>
        public string UniversityGroup { get; set; }

        public string? Keyword { get; set; }
    }
}
