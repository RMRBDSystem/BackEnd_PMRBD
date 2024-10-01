using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class RecipeRate
{
    public int RecipeId { get; set; }

    public int CustomerId { get; set; }

    public int? RatePoint { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
