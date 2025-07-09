using System;

namespace GenderHealthcare.BLL.DTOs
{
    public class MenstrualCycleDTO
    {
        public string Id { get; set; }               // Khóa chính
        public string UserId { get; set; }           // FK tới Users
        public DateOnly? CycleStartDate { get; set; }
        public DateOnly? CycleEndDate { get; set; }
        public int? CycleLength { get; set; }
        public int? PeriodLength { get; set; }
        public int? FlowIntensity { get; set; }
        public int? PainLevel { get; set; }
        public string? Notes { get; set; }
    }
}
