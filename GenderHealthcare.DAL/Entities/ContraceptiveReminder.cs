using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class ContraceptiveReminder
{
    public string UserId { get; set; } = null!;

    public string? ContraceptiveType { get; set; }

    public TimeOnly? ReminderTime { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Frequency { get; set; }

    public string? ReminderStatus { get; set; }

    public string? ReminderMessage { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
