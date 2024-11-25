using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IotRemoteLab.Domain.Schedule;

public abstract class ScheduleBase<T> : ScheduleBase where T : IScheduleHolder
{
    /// <summary>
    /// Держатель
    /// </summary>
    public abstract T GetHolder();
}

public abstract class ScheduleBase
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }
    public IEnumerable<Stand.Stand> Stands { get; init; }
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
}