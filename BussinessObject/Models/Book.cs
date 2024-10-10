using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? BookName { get; set; }

    public string? Description { get; set; }

    public int? Price { get; set; }

    public int weight { get; set; }

    public int length { get; set; }

    public int width { get; set; }

    public int height { get; set; }

    public string? Required_note { get; set; }

    public string? Address { get; set; }

    public int? UnitInStock { get; set; }

    public string? Ward_code { get; set; }

    public string? District_code { get; set; }

    public string? Province_code { get; set; }

    public int? Status { get; set; }

    public int? CensorId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateById { get; set; }

    public int? CategoryId { get; set; }

    public bool? Isbn { get; set; }

    public virtual ICollection<BookOrder> BookOrder { get; set; } = new List<BookOrder>();

    public virtual ICollection<BookRate> BookRates { get; set; } = new List<BookRate>();

    public virtual BookCategory? Category { get; set; }

    public virtual Employee? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Customer? CreateBy { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
