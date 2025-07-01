using System;

namespace GenderHealthcare.BLL.DTOs
{
    public class AppointmentDTO
    {
        public string UserId { get; set; }
        public string ConsultantId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? AppointmentStatus { get; set; }
        public string? AppointmentLocation { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public decimal? FixedPrice { get; set; }
        public bool? Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}