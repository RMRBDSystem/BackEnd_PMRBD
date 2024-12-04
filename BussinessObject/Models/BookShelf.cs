using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class BookShelf
{
    public int CustomerId { get; set; }

    public int EbookId { get; set; }

    public int? RatePoint { get; set; }

    public decimal? PurchasePrice { get; set; }

    public DateTime? PurchaseDate { get; set; } = DateTime.Now;

    public int? Status { get; set; }


    public virtual Account? Customer { get; set; }


    public virtual Ebook? Ebook { get; set; }
}
