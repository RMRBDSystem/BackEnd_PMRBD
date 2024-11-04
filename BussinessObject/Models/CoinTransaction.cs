using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class CoinTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CoinTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? MoneyFluctuations { get; set; }

    public int? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; }

    public int? Status { get; set; }

    public virtual Account? Customer { get; set; }
}
