using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models;

public partial class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AccountId { get; set; }

    public string Email { get; set; } = null!;

    public string GoogleId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public int? Coin { get; set; }

    public int? RoleId { get; set; }

    public int? AccountStatus { get; set; }

    public virtual AccountProfile? AccountProfileAccount { get; set; }

    public virtual ICollection<AccountProfile> AccountProfileCensors { get; set; } = new List<AccountProfile>();

    public virtual ICollection<Book> BookCensors { get; set; } = new List<Book>();

    public virtual ICollection<Book> BookCreateBies { get; set; } = new List<Book>();

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<BookRate> BookRates { get; set; } = new List<BookRate>();

    public virtual ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();

    public virtual ICollection<BookTransaction> BookTransactions { get; set; } = new List<BookTransaction>();

    public virtual ICollection<CoinTransaction> CoinTransactions { get; set; } = new List<CoinTransaction>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<Ebook> EbookCensors { get; set; } = new List<Ebook>();

    public virtual ICollection<Ebook> EbookCreateBies { get; set; } = new List<Ebook>();

    public virtual ICollection<EbookTransaction> EbookTransactions { get; set; } = new List<EbookTransaction>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PersonalRecipe> PersonalRecipes { get; set; } = new List<PersonalRecipe>();

    public virtual ICollection<Recipe> RecipeCensors { get; set; } = new List<Recipe>();

    public virtual ICollection<Recipe> RecipeCreateBies { get; set; } = new List<Recipe>();

    public virtual ICollection<RecipeTransaction> RecipeTransactions { get; set; } = new List<RecipeTransaction>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<ServiceFeedback> ServiceFeedbackCustomers { get; set; } = new List<ServiceFeedback>();

    public virtual ICollection<ServiceFeedback> ServiceFeedbackEmployees { get; set; } = new List<ServiceFeedback>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<RecipeRate> RecipeRates { get; set; } = new List<RecipeRate>();
}
