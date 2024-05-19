using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.API.Controllers;

public class ScheduleDto
{
    [Required] public Guid TeamId { get; set; }
    
    [Required] public Guid[] Stands { get; init; }
    
    [Required] public long Start { get; init; }
    [Required] public long End { get; init; }
}