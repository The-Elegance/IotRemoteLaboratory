using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Domain.Team;

public record Team
{
    [Required] 
    public Guid Id { get; set; }
    [Required] 
    public  string Name { get; set; }
    public  IEnumerable<User.User> Members  { get; set; }
}
