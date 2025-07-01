using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class Payment
{
    public string Id { get; set; } = null!;

    public string? UserId { get; set; }

    public string? AppointmentUserId { get; set; }

    public string? AppointmentConsultantId { get; set; }

    public DateTime? AppointmentDate { get; set; }

    public string? OfferId { get; set; }

    public decimal? OriginalAmount { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? Amount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime? PaymentDate { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Offer? Offer { get; set; }

    public virtual User? User { get; set; }
}
