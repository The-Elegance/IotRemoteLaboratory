using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Domain.User;
public record User
{
    [Required] public Guid Id { get; set; }
    [Required]  public  string Email{ get; set; }
    [Required] public  string PasswordHash{ get; set; }
    [Required] public  string Name{ get; set; }
    public  string? Surname{ get; set; }
    [Required] public  string? GroupNumber{ get; set; }
    [Required] public  IReadOnlyList<Role.Role> Roles { get; set; }
}