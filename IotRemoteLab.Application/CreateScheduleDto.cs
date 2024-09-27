using System.ComponentModel.DataAnnotations;

namespace IotRemoteLab.Application;

public record CreateScheduleDto
{
    [Required] public Guid TeamId { get; set; }
    
    [Required] public long[] Stands { get; init; }
    
    [Required] public long Start { get; init; }
    [Required] public long End { get; init; }
}