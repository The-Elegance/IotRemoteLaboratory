namespace IotRemoteLab.Domain;

public sealed record Schedule
{
    public Guid Id { get; init; }
    public Team.Team Team { get; init; }
    public IReadOnlyList<Stand.Stand> Stands { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
}