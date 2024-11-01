using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BookRate
{
    public int RatePoint { get; set; }

    public int CustomerId { get; set; }

    public int BookId { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Account? Customer { get; set; }
}
