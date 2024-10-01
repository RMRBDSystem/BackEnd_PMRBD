using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Ebook
{
    public int EbookId { get; set; }

    public string? EbookName { get; set; }

    public string? Description { get; set; }

    public int? Price { get; set; }

    public int? Status { get; set; }

    public string? Pdfurl { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CensorId { get; set; }

    public int? CreateById { get; set; }

    public int? CategoryId { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();

    public virtual BookCategory? Category { get; set; }

    public virtual Employee? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Customer? CreateBy { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
