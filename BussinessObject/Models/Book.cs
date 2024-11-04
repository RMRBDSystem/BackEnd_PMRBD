using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookId { get; set; }

    public string BookName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int UnitInStock { get; set; }

    public int? Status { get; set; }

    public int? CensorId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? CreateById { get; set; }

    public int? CategoryId { get; set; }

    public string? Isbn { get; set; }

    public int? Weight { get; set; }

    public int? Length { get; set; }

    public int? Width { get; set; }

    public int? Height { get; set; }

    public string RequiredNote { get; set; } = null!;

    public int? SenderAddressId { get; set; }

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<BookRate> BookRates { get; set; } = new List<BookRate>();

    public virtual BookCategory? Category { get; set; }

    public virtual Account? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Account? CreateBy { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual CustomerAddress? SenderAddress { get; set; }
}
