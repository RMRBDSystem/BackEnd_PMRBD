﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class EbookTransaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EbookTransactionId { get; set; }

    public int? CustomerId { get; set; }

    public int? EbookId { get; set; }

    public decimal? CoinFluctuations { get; set; }

    public DateTime? Date { get; set; } = DateTime.Now;

    public string? Details { get; set; }

    public int? Status { get; set; }


    public virtual Account? Customer { get; set; }


    public virtual Ebook? Ebook { get; set; }
}
