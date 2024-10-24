using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class BookOrder
{
    [Key]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int BookId { get; set; }

    public decimal? Price { get; set; }

    public decimal? ShipFee { get; set; }

    public int? Quantity { get; set; }

    public int Status { get; set; }

    public decimal? TotalPrice { get; set; }
   
    public string Order_Code { get; set; }

    // 1: thanh toan bang coin, 2: thanh toan bang tien mat

    public int? PurchaseMethod { get; set; }

    public string PhoneNumber { get; set; }

    public DateTime? PurchaseDate { get; set; }

    // Ma phuong 
    public string Ward_code { get; set; }

    // Ma huyen
    public string District_Id { get; set; }

    // Ben tra phi ship(1: He thong, 2: Khach hang)
    public int PaymentType { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Book? Book { get; set; } = null!;

    public virtual ICollection<BookTransaction> BookTransactions { get; set; } = new List<BookTransaction>();

    public virtual ICollection<BookOrderStatus> BookOrderStatuses { get; set; } = new List<BookOrderStatus>();
}
