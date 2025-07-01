using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class Appointment
{
    public string UserId { get; set; } = null!;

    public string ConsultantId { get; set; } = null!;

    public DateTime AppointmentDate { get; set; }

    public string? AppointmentStatus { get; set; }

    public string? AppointmentLocation { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public decimal? FixedPrice { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Consultant { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
