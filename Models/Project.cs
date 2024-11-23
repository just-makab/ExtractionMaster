using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public int FacilityId { get; set; }

    public int UserId { get; set; }

    public DateOnly SurveyDate { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public decimal? TotalCost { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public string ProgressStatus { get; set; } = null!;

    public virtual Facility Facility { get; set; } = null!;

    public virtual ICollection<FacilityMaintenance> FacilityMaintenances { get; set; } = new List<FacilityMaintenance>();

    public virtual ICollection<JobCard> JobCards { get; set; } = new List<JobCard>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual User User { get; set; } = null!;
}
