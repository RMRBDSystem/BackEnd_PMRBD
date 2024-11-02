using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class Image
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ImageId { get; set; }

    public int? RecipeId { get; set; }

    public int? BookId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public int? Status { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
