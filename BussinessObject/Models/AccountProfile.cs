using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public partial class AccountProfile
{
    [Key]
    public int AccountId { get; set; }

    public string? FrontIdcard { get; set; }

    public string? BackIdcard { get; set; }

    public string? Portrait { get; set; }

    public string? Avatar { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? IdcardNumber { get; set; }

    public int? CensorId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Account? Censor { get; set; }
}
