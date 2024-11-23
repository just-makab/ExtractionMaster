using System;
using System.Collections.Generic;

namespace EM_WebApp.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ProjectId { get; set; }

    public DateOnly PaymentDate { get; set; }

    public decimal AmountPaid { get; set; }

    public virtual Project Project { get; set; } = null!;
}
