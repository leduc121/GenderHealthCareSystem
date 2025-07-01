using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class StdTestAppointment
{
    public string UserId { get; set; } = null!;

    public string DiseaseId { get; set; } = null!;

    public DateTime TestDate { get; set; }

    public string? TestStatus { get; set; }

    public string? TestLocation { get; set; }

    public string? OfferId { get; set; }

    public decimal? OriginalPrice { get; set; }

    public decimal? DiscountAmount { get; set; }

    public decimal? TestPrice { get; set; }

    public DateTime? ResultDate { get; set; }

    public string? TestResult { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual StdDisease Disease { get; set; } = null!;

    public virtual Offer? Offer { get; set; }

    public virtual User User { get; set; } = null!;
}
