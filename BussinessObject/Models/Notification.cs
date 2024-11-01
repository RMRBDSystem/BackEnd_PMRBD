using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Notification
{
    public int NotificationId { get; set; }

    public int? AccountId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public int? Status { get; set; }

    public virtual Account? Account { get; set; }
}
