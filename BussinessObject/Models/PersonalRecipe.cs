using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class PersonalRecipe
{
    public int RecipeId { get; set; }

    public int CustomerId { get; set; }

    public int? NumberOfService { get; set; }

    public string? Nutrition { get; set; }

    public string? Tutorial { get; set; }

    public string? Ingredient { get; set; }

    public decimal? PurchasePrice { get; set; }

    public int? Status { get; set; }

    [JsonIgnore]
    public virtual Account? Account { get; set; }

    //[JsonIgnore]
    public virtual Recipe? Recipe { get; set; }
}
