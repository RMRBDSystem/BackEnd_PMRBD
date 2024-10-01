using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Image
{
    public int ImageId { get; set; }

    public int? RecipeId { get; set; }

    public int? BookId { get; set; }

    public string? ImageUrl { get; set; }

    public int? Status { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
