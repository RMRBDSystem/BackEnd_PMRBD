using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class BookOrderStatus
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookOrderStatusId { get; set; }

    public int? OrderId { get; set; }

    public int? Status { get; set; }

    public DateTime? StatusDate { get; set; } = DateTime.Now;

    public string? Details { get; set; }

    public virtual BookOrder? Order { get; set; }
}
