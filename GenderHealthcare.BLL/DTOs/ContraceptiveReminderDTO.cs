using System;

namespace GenderHealthcare.BLL.DTOs
{
    public class ContraceptiveReminderDTO
    {
        public string UserId { get; set; } = null!;
        public string? ContraceptiveType { get; set; }
        public TimeOnly? ReminderTime { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? Frequency { get; set; }
        public string? ReminderStatus { get; set; }
        public string? ReminderMessage { get; set; }
        public bool? Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
