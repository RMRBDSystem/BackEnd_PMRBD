using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class BookRate
{
    public int RatePoint { get; set; }

    public int CustomerId { get; set; }

    public int BookId { get; set; }

    [JsonIgnore]
    public virtual Book? Book { get; set; }

    [JsonIgnore]
    public virtual AccountDTO? Customer { get; set; }
}
