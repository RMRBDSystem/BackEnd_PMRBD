using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BookOrderDetail
{
    public int OrderId { get; set; }

    public int BookId { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual BookOrder Order { get; set; } = null!;
}
