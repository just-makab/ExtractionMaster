using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class JobCard
{
    public int JobCardId { get; set; }

    public int ProjectId { get; set; }

    public string? Instructions { get; set; }

    public string? Notes { get; set; }

    public DateOnly? JobStart { get; set; }

    public DateOnly? CompletionDate { get; set; }

    public virtual ICollection<JobCardTeam> JobCardTeams { get; set; } = new List<JobCardTeam>();

    public virtual Project Project { get; set; } = null!;
}
