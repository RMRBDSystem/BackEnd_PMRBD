using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? EmployeeId { get; set; }

    public int? CustomerId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public int? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }
}
