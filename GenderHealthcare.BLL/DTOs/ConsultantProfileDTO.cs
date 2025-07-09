using System;

namespace GenderHealthcare.BLL.DTOs
{
    public class ConsultantProfileDTO
    {


        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public string ConsultantId { get; set; }
        public string? Specialization { get; set; }
        public string? Qualification { get; set; }
        public string? Experience { get; set; }
        public decimal? ConsultationFee { get; set; }
        public bool? IsAvailable { get; set; }
        public string? ProfileStatus { get; set; }
        public bool? Status { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}