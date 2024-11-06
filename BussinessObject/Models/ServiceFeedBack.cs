using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class ServiceFeedback
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FeedBackId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? Date { get; set; }

    public int? Status { get; set; }

    public string? Response { get; set; }

    public int? EmployeeId { get; set; }

    public int? CustomerId { get; set; }

    public virtual Account? Customer { get; set; }

    public virtual Account? Employee { get; set; }
}
