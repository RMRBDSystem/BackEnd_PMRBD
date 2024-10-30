using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class PhoneNumber
{
    public int PhoneNumberId { get; set; }

    public int? AccountId { get; set; }

    public string? Number { get; set; }

    public int? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();
}
