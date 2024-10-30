using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Ebook
{
    public int EbookId { get; set; }

    public string EbookName { get; set; } = null!;

    public string? Description { get; set; }

    public int? Price { get; set; }

    public int? Status { get; set; }

    public string Pdfurl { get; set; } = null!;

    public DateTime? CreateDate { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int? CreateById { get; set; }

    public int? CensorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();

    public virtual BookCategory? Category { get; set; }

    public virtual Account? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Account? CreateBy { get; set; }

    public virtual ICollection<EbookTransaction> EbookTransactions { get; set; } = new List<EbookTransaction>();
}
