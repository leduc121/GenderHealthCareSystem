using System;

namespace GenderHealthcare.UI.ViewModels
{
    public class AppointmentVM
    {
        public string ConsultantId { get; set; } = string.Empty;
        public string ConsultantName { get; set; } = string.Empty;
        public DateTime AppointmentDate { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public string AppointmentLocation { get; set; } = string.Empty;
    }
}


