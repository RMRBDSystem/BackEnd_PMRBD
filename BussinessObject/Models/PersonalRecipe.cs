﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class PersonalRecipe
{
    public int RecipeId { get; set; }

    public int CustomerId { get; set; }

    public int? NumberofService { get; set; }

    public string? Nutrition { get; set; }

    public string? Tutorial { get; set; }

    public int? Status { get; set; }

    public int? PurchasePrice { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
