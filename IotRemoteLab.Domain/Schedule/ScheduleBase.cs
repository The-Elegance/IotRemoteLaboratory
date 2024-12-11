namespace IotRemoteLab.Domain.Schedule;

public abstract class ScheduleBase
{
    public Guid Id { get; init; }
    public HashSet<Stand.Stand> Stands { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    /// <summary>
    /// Приоритет
    /// </summary>
    public SchedulePriority Priority { get; set; }

    /// <summary>
    /// Id держателя
    /// </summary>
    public Guid HolderId { get; set; }
    /// <summary>
    /// Держатель
    /// </summary>

    public abstract T GetHolder<T>() where T : IScheduleHolder;
}