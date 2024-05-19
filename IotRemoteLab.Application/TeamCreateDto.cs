namespace IotRemoteLab.Application;

public class TeamCreateDto
{
    public string TeamName { get; init; }

    public Guid[] UserIds { get; init; }
}