using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? OfficeLocation { get; set; }

    public string? Role { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
