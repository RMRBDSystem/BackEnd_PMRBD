using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class CoinTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CoinTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? MoneyFluctuations { get; set; }

    public decimal? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; } = DateTime.Now;

    public int? Status { get; set; }

    [JsonIgnore]
    public virtual Account? Customer { get; set; }
}
