namespace IotRemoteLab.Domain.Schedule
{
    public class TeamHolderSchedule : ScheduleBase
    {
        public Team Holder { get; set; }

        public override T GetHolder<T>()
        {
            throw new NotImplementedException();
        }
    }
}
