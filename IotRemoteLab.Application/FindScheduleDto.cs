namespace IotRemoteLab.Application;

public class FindScheduleDto
{
    public Guid? TeamId { get; init; }
    public Guid? StandId { get; init; }
    public long? Start { get; init; }
    public long? End { get; init; }
}