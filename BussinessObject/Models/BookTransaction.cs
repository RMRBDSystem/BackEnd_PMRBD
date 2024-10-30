using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BookTransaction
{
    public int BookTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public int? OrderId { get; set; }

    public decimal? MoneyFluctuations { get; set; }

    public int? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; }

    public string? Details { get; set; }

    public int? Status { get; set; }

    public virtual Account? Customer { get; set; }

    public virtual BookOrder? Order { get; set; }
}
