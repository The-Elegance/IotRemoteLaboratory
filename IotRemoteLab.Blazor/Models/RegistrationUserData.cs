using IotRemoteLab.Blazor.Resources;
using IotRemoteLab.Domain;
using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Blazor.Models
{
    public class RegistrationUserData
    {
        [Required(ErrorMessageResourceName = "EmailRequired", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [EmailAddress(ErrorMessageResourceName = "WrongEmailFormat", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string Email { get; set; }
        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [MinLength(6, ErrorMessage = "Минимальная длина пароля 6 символа")]
        public string Password { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessageResourceName = "SurnameRequired", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string Surname { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [Required(ErrorMessageResourceName = "MiddleNameRequired", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string MiddleName { get; set; }
        /// <summary>
        /// Университет
        /// </summary>
        [Required(ErrorMessageResourceName = "UniversityRequired", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public Guid UniversityId { get; set; }
        /// <summary>
        /// Учебная группа
        /// </summary>
        [Required(ErrorMessageResourceName = "AcademyGroupRequired", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public Guid AcademyGroupId { get; set; }

        public string? Keyword { get; set; }
    }
}
