namespace IotRemoteLab.Domain.Schedule
{
    public class UserHolderSchedule : ScheduleBase
    {
        public User Holder { get; set; }

        public override T GetHolder<T>()
        {
            throw new NotImplementedException();
        }
    }
}
