using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class ConsultantFeedback
{
    public string ConsultantId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime FeedbackDate { get; set; }

    public int Rating { get; set; }

    public string? FeedbackContent { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Consultant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
