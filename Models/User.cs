using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
