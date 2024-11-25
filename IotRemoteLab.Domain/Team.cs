using IotRemoteLab.Domain.Schedule;
using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Domain;

public class Team : IScheduleHolder
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public ICollection<User> Members { get; set; }
}
