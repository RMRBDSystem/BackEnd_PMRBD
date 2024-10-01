using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public string? Description { get; set; }

    public int? NumberofService { get; set; }

    public string? Nutrition { get; set; }

    public string? Tutorial { get; set; }

    public string? Video { get; set; }

    public int? CreateById { get; set; }

    public int? Price { get; set; }

    public string? Ingredient { get; set; }

    public int? Status { get; set; }

    public int? CensorId { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? TotalTime { get; set; }

    public virtual Employee? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Customer? CreateBy { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<PersonalRecipe> PersonalRecipes { get; set; } = new List<PersonalRecipe>();

    public virtual ICollection<RecipeRate> RecipeRates { get; set; } = new List<RecipeRate>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
