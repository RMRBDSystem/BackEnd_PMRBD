
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class AccountProfile
{
    [Key]
    public int AccountId { get; set; }

    public string? FrontIdcard { get; set; }

    public string? BackIdcard { get; set; }

    public string? Portrait { get; set; }

    public string? BankAccountQR { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? IdcardNumber { get; set; }

    public int? CensorId { get; set; }

    public int Status { get; set; } = -1;

    [JsonIgnore]
    public virtual Account? Account { get; set; }

    [JsonIgnore]
    public virtual Account? Censor { get; set; }
}
