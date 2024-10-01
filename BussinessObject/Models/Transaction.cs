using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? CustomerId { get; set; }

    public int? RecipeId { get; set; }

    public int? OrderId { get; set; }

    public int? EbookId { get; set; }

    public decimal? MoneyFluctuations { get; set; }

    public decimal? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; }

    public string? Details { get; set; }

    public int? Status { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Ebook? Ebook { get; set; }

    public virtual BookOrder? Order { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
