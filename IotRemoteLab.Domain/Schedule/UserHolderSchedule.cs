namespace IotRemoteLab.Domain.Schedule
{
    public class UserHolderSchedule : ScheduleBase<User>
    {
        public User Holder { get; set; }

        public override User GetHolder()
        {
            return Holder;
        }
    }
}
