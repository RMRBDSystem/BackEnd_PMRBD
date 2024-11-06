using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObject.Models;

public partial class Recipe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecipeId { get; set; }

    public string RecipeName { get; set; } = null!;

    public string? Description { get; set; }

    public int? NumberOfService { get; set; }

    public string? Nutrition { get; set; }

    public string? Tutorial { get; set; }

    public string? Video { get; set; }

    public int? CreateById { get; set; }

    public decimal? Price { get; set; }

    public string? Ingredient { get; set; }

    public int? CensorId { get; set; }

    public int Status { get; set; } = -1;

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public int? TotalTime { get; set; }

    [JsonIgnore]
    public virtual Account? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [JsonIgnore]
    public virtual Account? CreateBy { get; set; }

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<PersonalRecipe> PersonalRecipes { get; set; } = new List<PersonalRecipe>();

    public virtual ICollection<RecipeTransaction> RecipeTransactions { get; set; } = new List<RecipeTransaction>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<RecipeTag> RecipeTags { get; set; } = new List<RecipeTag>();

    public virtual ICollection<RecipeRate> RecipeRates { get; set; } = new List<RecipeRate>();
}
