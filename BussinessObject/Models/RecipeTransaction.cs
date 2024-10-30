using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class RecipeTransaction
{
    public int RecipeTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public int? RecipeId { get; set; }

    public int? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; }

    public string? Detail { get; set; }

    public int? Status { get; set; }

    public virtual Account? Customer { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
