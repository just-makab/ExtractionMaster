using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class FacilityMaintenance
{
    public int FacilityMaintenanceId { get; set; }

    public int ProjectId { get; set; }

    public DateOnly MaintenanceDate { get; set; }

    public DateOnly NextDueDate { get; set; }

    public virtual Project Project { get; set; } = null!;
}
