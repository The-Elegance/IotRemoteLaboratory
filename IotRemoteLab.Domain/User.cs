using IotRemoteLab.Domain.Schedule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IotRemoteLab.Domain;
public class User : IScheduleHolder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? GroupNumber { get; set; }
    [Required]
    public IReadOnlyList<Role.Role> Roles { get; set; }
}