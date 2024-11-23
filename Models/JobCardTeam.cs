using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class JobCardTeam
{
    public int JobCardTeamId { get; set; }

    public int JobCardId { get; set; }

    public int EmployeeId { get; set; }

    public string TeamName { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobCard JobCard { get; set; } = null!;
}
