using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class BookOrder
{
    [Key]
    public int OrderId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public int? PurchaseMethod { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<BookOrderDetail> BookOrderDetails { get; set; } = new List<BookOrderDetail>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
