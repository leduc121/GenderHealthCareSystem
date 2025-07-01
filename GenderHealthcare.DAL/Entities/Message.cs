using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class Message
{
    public string UserId { get; set; } = null!;

    public string ConsultantId { get; set; } = null!;

    public string MessageContent { get; set; } = null!;

    public string SenderType { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Consultant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
