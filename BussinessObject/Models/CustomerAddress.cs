using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class CustomerAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressId { get; set; }

    public int? AccountId { get; set; }

    public string? PhoneNumber { get; set; }

    public int? AddressStatus { get; set; }

    public int WardCode { get; set; }

    public int DistrictCode { get; set; }

    public int ProvinceCode { get; set; }

    public string? AddressDetail { get; set; }

    public int? PhoneNumberId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
