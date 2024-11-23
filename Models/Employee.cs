using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<JobCardTeam> JobCardTeams { get; set; } = new List<JobCardTeam>();
}
