using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BookOrderStatus
{
    public int BookOrderStatusId { get; set; }

    public int? OrderId { get; set; }

    public int? Status { get; set; }

    public DateTime? StatusDate { get; set; }

    public string? Details { get; set; }

    public virtual BookOrder? Order { get; set; }
}
