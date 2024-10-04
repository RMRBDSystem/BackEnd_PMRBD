using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public partial class BookCategory
{
    [Key]
    public int CategoryId { get; set; }

    public string? Name { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual ICollection<Ebook> Ebooks { get; set; } = new List<Ebook>();
}
