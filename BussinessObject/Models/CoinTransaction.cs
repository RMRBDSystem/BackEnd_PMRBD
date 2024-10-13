using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class CoinTransaction
{
    public int CoinTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? MoneyFluctuations { get; set; }

    public int? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; }

    public string? Details { get; set; }

    public int? Status { get; set; }

    public virtual Customer? Customer { get; set; }
}
