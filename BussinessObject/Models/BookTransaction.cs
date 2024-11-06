using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class BookTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public int? OrderId { get; set; }

    public decimal? MoneyFluctuations { get; set; }

    public decimal? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; }

    public string? Details { get; set; }

    public int? Status { get; set; }

    [JsonIgnore]
    public virtual Account? Customer { get; set; }

    [JsonIgnore]
    public virtual BookOrder? Order { get; set; }
}
