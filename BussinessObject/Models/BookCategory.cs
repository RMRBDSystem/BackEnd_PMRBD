using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class BookCategory
{
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Ebook> Ebooks { get; set; } = new List<Ebook>();
}
