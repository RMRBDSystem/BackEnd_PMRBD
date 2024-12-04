using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class BookOrder
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? ShipFee { get; set; }

    public decimal? Price { get; set; }

    public int? PurchaseMethod { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? PaymentType { get; set; }

    public string? OrderCode { get; set; }

    public int? Status { get; set; }

    public int? ClientAddressId { get; set; }

    public virtual ICollection<BookOrderStatus> BookOrderStatuses { get; set; } = new List<BookOrderStatus>();

    public virtual ICollection<BookTransaction> BookTransactions { get; set; } = new List<BookTransaction>();

    public virtual CustomerAddress? ClientAddress { get; set; }

    public virtual ICollection<BookOrderDetail> BookOrderDetails { get; set; } = new List<BookOrderDetail>();

    public virtual Account? Customer { get; set; }
}
