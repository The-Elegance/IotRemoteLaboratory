namespace IotRemoteLab.Domain.Schedule
{
    public class TeamHolderSchedule : ScheduleBase<Team>
    {
        public Team Holder { get; set; }

        public override Team GetHolder()
        {
            return Holder;
        }
    }
}
