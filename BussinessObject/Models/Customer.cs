using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Email { get; set; } = null!;

    public string GoogleId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Avatar { get; set; }

    public int? Coin { get; set; }

    public int? AccountStatus { get; set; }

    public string? FrontIdcard { get; set; }

    public string? BackIdcard { get; set; }

    public string? Portrait { get; set; }

    public string? IdcardNumber { get; set; }

    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int SellerStatus { get; set; }

    public int? CensorId { get; set; }

    public virtual ICollection<BookOrder> BookOrders { get; set; } = new List<BookOrder>();

    public virtual ICollection<BookRate> BookRates { get; set; } = new List<BookRate>();

    public virtual ICollection<BookShelf> BookShelves { get; set; } = new List<BookShelf>();

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    public virtual Employee? Censor { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Ebook> Ebooks { get; set; } = new List<Ebook>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<PersonalRecipe> PersonalRecipes { get; set; } = new List<PersonalRecipe>();

    public virtual ICollection<RecipeRate> RecipeRates { get; set; } = new List<RecipeRate>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<ServiceFeedBack> ServiceFeedBacks { get; set; } = new List<ServiceFeedBack>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
