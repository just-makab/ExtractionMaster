using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class Facility
{
    public int FacilityId { get; set; }

    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? OperatingHours { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
