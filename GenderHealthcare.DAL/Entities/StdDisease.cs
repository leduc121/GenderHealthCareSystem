using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class StdDisease
{
    public string Id { get; set; } = null!;

    public string DiseaseName { get; set; } = null!;

    public decimal TestPrice { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<StdTestAppointment> StdTestAppointments { get; set; } = new List<StdTestAppointment>();
}
