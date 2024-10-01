using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BookRate
{
    public int CustomerId { get; set; }

    public int BookId { get; set; }

    public int? RatePoint { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;
}
