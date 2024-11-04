using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class Tag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TagId { get; set; }

    public string TagName { get; set; } = null!;

    public int? Status { get; set; }

    public virtual ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();
}
