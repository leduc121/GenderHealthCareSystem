using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class Role
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }


    public ICollection<UserRole> UserRoles { get; set; }
}
