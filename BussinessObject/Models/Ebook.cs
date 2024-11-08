using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class Ebook
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EbookId { get; set; }

    public string EbookName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int? Status { get; set; }

    public string? Pdfurl { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string? ImageUrl { get; set; } = null!;

    public int? CreateById { get; set; }

    public int? CensorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();

    [JsonIgnore]
    public virtual BookCategory? Category { get; set; }

    [JsonIgnore]
    public virtual Account? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual Account? CreateBy { get; set; }

    public virtual ICollection<EbookTransaction> EbookTransactions { get; set; } = new List<EbookTransaction>();
}
