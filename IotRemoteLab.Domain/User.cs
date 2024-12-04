using IotRemoteLab.Domain.Schedule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IotRemoteLab.Domain;
public class User : IScheduleHolder
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    /// <summary>
    /// Электронная почта
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Пароль
    /// </summary>
    public string PasswordHash { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public string? Surname { get; set; }
    /// <summary>
    /// Id учебного заведения
    /// </summary>
    public Guid UniversityId { get; set; }
    /// <summary>
    /// Id академической группы
    /// </summary>
    public Guid AcademyGroupId { get; set; }
    /// <summary>
    /// Роли пользователя
    /// </summary>
    public IReadOnlyList<Role.Role> Roles { get; set; }
}