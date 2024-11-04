using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class Notification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NotificationId { get; set; }

    public int? AccountId { get; set; }

    public string? Content { get; set; }

    public DateTime? Date { get; set; }

    public int? Status { get; set; }

    public virtual Account? Account { get; set; }
}
