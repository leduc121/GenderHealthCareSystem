using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class ConsultantProfile
{
    public string ConsultantId { get; set; } = null!;

    public string? Specialization { get; set; }

    public string? Qualification { get; set; }

    public string? Experience { get; set; }

    public decimal? ConsultationFee { get; set; }

    public bool? IsAvailable { get; set; }

    public string? ProfileStatus { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Consultant { get; set; } = null!;
}
