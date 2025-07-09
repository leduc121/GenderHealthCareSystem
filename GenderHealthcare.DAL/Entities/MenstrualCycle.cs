using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class MenstrualCycle
{

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = null!;

    public DateOnly? CycleStartDate { get; set; }

    public DateOnly? CycleEndDate { get; set; }

    public int? CycleLength { get; set; }

    public int? PeriodLength { get; set; }

    public int? FlowIntensity { get; set; }

    public int? PainLevel { get; set; }

    public string? Notes { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
