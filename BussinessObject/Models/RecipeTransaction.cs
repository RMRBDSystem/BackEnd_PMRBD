using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class RecipeTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecipeTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public int? RecipeId { get; set; }

    public decimal? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; }

    public string? Detail { get; set; }

    public int? Status { get; set; }

    public virtual Account? Customer { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
