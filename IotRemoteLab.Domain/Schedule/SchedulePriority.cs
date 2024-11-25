namespace IotRemoteLab.Domain.Schedule
{
    public enum SchedulePriority
    {
        /// <summary>
        /// Без записи
        /// </summary>
        Solo,
        /// <summary>
        /// Запись командой
        /// </summary>
        Team,
        /// <summary>
        /// Проведение занятия
        /// </summary>
        Class
    }
}
