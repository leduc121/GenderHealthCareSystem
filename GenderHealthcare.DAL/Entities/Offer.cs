using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class Offer
{
    public string Id { get; set; } = null!;

    public string OfferName { get; set; } = null!;

    public string OfferType { get; set; } = null!;

    public decimal? DiscountValue { get; set; }

    public decimal? MinAmount { get; set; }

    public decimal? MaxDiscount { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? UsageLimit { get; set; }

    public int? UsedCount { get; set; }

    public string? ApplicableServices { get; set; }

    public bool? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<StdTestAppointment> StdTestAppointments { get; set; } = new List<StdTestAppointment>();
}
