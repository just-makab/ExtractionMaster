using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int CustomerId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime ScheduledSendTime { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
