using System;

namespace GenderHealthcare.UI.Services
{
    /// <summary>
    /// Interface cho việc lên lịch và hiển thị nhắc nhở bên client.
    /// </summary>
    public interface IReminderService
    {
        void ScheduleOneTime(DateTime triggerTime, string message);
        void ScheduleDaily(TimeSpan timeOfDay, string message);
        void ClearAll();
    }
}