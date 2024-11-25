using IotRemoteLab.Domain;
using IotRemoteLab.Domain.Stand;

namespace IotRemoteLab.Application;

public class ScheduleDto
{
    public Guid Id { get; init; }
    public Team Team { get; init; }
    public IReadOnlyList<Stand> Stands { get; init; }
    public long Start { get; init; }
    public long End { get; init; }
    
}