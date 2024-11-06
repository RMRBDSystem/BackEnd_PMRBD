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

    public DateTime? PurchaseDate { get; set; }

    public int? Status { get; set; }

    [JsonIgnore]
    public virtual Account? Customer { get; set; }

    [JsonIgnore]
    public virtual Ebook? Ebook { get; set; }
}
