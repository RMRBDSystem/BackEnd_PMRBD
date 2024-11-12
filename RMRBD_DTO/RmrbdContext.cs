using System;
using System.Collections.Generic;
using BussinessObject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Models;

public partial class RmrbdContext : DbContext
{
    public RmrbdContext()
    {
    }

    public RmrbdContext(DbContextOptions<RmrbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountDTO> Accounts { get; set; }

    public virtual DbSet<AccountProfile> AccountProfiles { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookCategory> BookCategories { get; set; }

    public virtual DbSet<BookOrder> BookOrders { get; set; }

    public virtual DbSet<BookOrderStatus> BookOrderStatuses { get; set; }

    public virtual DbSet<BookRate> BookRates { get; set; }

    public virtual DbSet<BookShelf> BookShelves { get; set; }

    public virtual DbSet<BookTransaction> BookTransactions { get; set; }

    public virtual DbSet<CoinTransaction> CoinTransactions { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<Ebook> Ebooks { get; set; }

    public virtual DbSet<EbookTransaction> EbookTransactions { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PersonalRecipe> PersonalRecipes { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeTransaction> RecipeTransactions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ServiceFeedback> ServiceFeedbacks { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<RecipeRate> RecipeRates { get; set; }

    public virtual DbSet<RecipeTag> RecipeTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging();
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountDTO>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("Account");

            entity.HasIndex(e => e.GoogleId).IsUnique();

            entity.HasIndex(e => e.Email).IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.Coin).HasDefaultValueSql("((0))");
            entity.Property(e => e.Email).HasMaxLength(500);
            entity.Property(e => e.GoogleId)
                .HasMaxLength(500)
                .HasColumnName("GoogleID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserName).HasMaxLength(500);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AccountProfile>(entity =>
        {
            entity.HasKey(e => e.AccountId);

            entity.ToTable("AccountProfile");

            entity.HasIndex(e => e.IdcardNumber).IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.BackIdcard).HasColumnName("BackIDCard");
            entity.Property(e => e.CensorId).HasColumnName("CensorID");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.FrontIdcard).HasColumnName("FrontIDCard");
            entity.Property(e => e.IdcardNumber)
                .HasMaxLength(30)
                .HasColumnName("IDCardNumber");

            entity.HasOne(d => d.Account).WithOne(p => p.AccountProfileAccount)
                .HasForeignKey<AccountProfile>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Censor).WithMany(p => p.AccountProfileCensors)
                .HasForeignKey(d => d.CensorId);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId);

            entity.ToTable("Book");

            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CensorId).HasColumnName("CensorID");
            entity.Property(e => e.CreateById).HasColumnName("CreateByID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.RequiredNote)
                .HasMaxLength(500)
                .HasColumnName("Required_Note");
            entity.Property(e => e.SenderAddressId).HasColumnName("SenderAddressID");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Censor).WithMany(p => p.BookCensors)
                .HasForeignKey(d => d.CensorId);

            entity.HasOne(d => d.CreateBy).WithMany(p => p.BookCreateBies)
                .HasForeignKey(d => d.CreateById);

            entity.HasOne(d => d.SenderAddress).WithMany(p => p.Books)
                .HasForeignKey(d => d.SenderAddressId);
        });

        modelBuilder.Entity<BookCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("BookCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<BookOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("BookOrder");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ClientAddressId).HasColumnName("ClientAddressID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.OrderCode)
                .HasMaxLength(500)
                .HasColumnName("Order_Code");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
            entity.Property(e => e.ShipFee).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Book).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.BookId);

            entity.HasOne(d => d.ClientAddress).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.ClientAddressId);

            entity.HasOne(d => d.Customer).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<BookOrderStatus>(entity =>
        {
            entity.HasKey(e => e.BookOrderStatusId).HasName("PK__BookOrde__967212521220FF6E");

            entity.ToTable("BookOrderStatus");

            entity.Property(e => e.BookOrderStatusId).HasColumnName("BookOrderStatusID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.StatusDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.BookOrderStatuses)
                .HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<BookRate>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.BookId }).HasName("PK__BookRate__C770689A26ADE04F");

            entity.ToTable("BookRate");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.BookId).HasColumnName("BookID");

            entity.HasOne(d => d.Book).WithMany(p => p.BookRates)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer).WithMany(p => p.BookRates)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<BookShelf>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.EbookId });

            entity.ToTable("BookShelf");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.PurchasePrice).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookShelves)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Ebook).WithMany(p => p.BookShelves)
                .HasForeignKey(d => d.EbookId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<BookTransaction>(entity =>
        {
            entity.HasKey(e => e.BookTransactionId);

            entity.ToTable("BookTransaction");

            entity.Property(e => e.BookTransactionId).HasColumnName("BookTransactionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MoneyFluctuations).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookTransactions)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Order).WithMany(p => p.BookTransactions)
                .HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<CoinTransaction>(entity =>
        {
            entity.HasKey(e => e.CoinTransactionId);

            entity.ToTable("CoinTransaction");

            entity.Property(e => e.CoinTransactionId).HasColumnName("CoinTransactionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MoneyFluctuations).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.CoinTransactions)
                .HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.RootCommentId).HasColumnName("RootCommentID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Book).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BookId);

            entity.HasOne(d => d.Customer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Ebook).WithMany(p => p.Comments)
                .HasForeignKey(d => d.EbookId);

            entity.HasOne(d => d.Recipe).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RecipeId);

            entity.HasOne(d => d.RootComment).WithMany(p => p.InverseRootComment)
                .HasForeignKey(d => d.RootCommentId);
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId);

            entity.ToTable("CustomerAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AddressDetail).HasMaxLength(500);
            entity.Property(e => e.AddressStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.DistrictCode).HasColumnName("District_Code");
            entity.Property(e => e.PhoneNumber).HasMaxLength(500);
            entity.Property(e => e.ProvinceCode).HasColumnName("Province_Code");
            entity.Property(e => e.WardCode).HasColumnName("Ward_Code");

            entity.HasOne(d => d.Account).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AccountId);
        });

        modelBuilder.Entity<Ebook>(entity =>
        {
            entity.HasKey(e => e.EbookId);

            entity.ToTable("EBook");

            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CensorId).HasColumnName("CensorID");
            entity.Property(e => e.CreateById).HasColumnName("CreateByID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EbookName).HasColumnName("EBookName");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Pdfurl).HasColumnName("PDFUrl");
            entity.Property(e => e.Price).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Category).WithMany(p => p.Ebooks)
                .HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.Censor).WithMany(p => p.EbookCensors)
                .HasForeignKey(d => d.CensorId);

            entity.HasOne(d => d.CreateBy).WithMany(p => p.EbookCreateBies)
                .HasForeignKey(d => d.CreateById);
        });

        modelBuilder.Entity<EbookTransaction>(entity =>
        {
            entity.HasKey(e => e.EbookTransactionId);

            entity.ToTable("EBookTransaction");

            entity.Property(e => e.EbookTransactionId).HasColumnName("EBookTransactionID");
            entity.Property(e => e.CoinFluctuations).HasDefaultValueSql("((0))");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.EbookTransactions)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Ebook).WithMany(p => p.EbookTransactions)
                .HasForeignKey(d => d.EbookId);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Book).WithMany(p => p.Images)
                .HasForeignKey(d => d.BookId);

            entity.HasOne(d => d.Recipe).WithMany(p => p.Images)
                .HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId);

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AccountId);
        });

        modelBuilder.Entity<PersonalRecipe>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.CustomerId });

            entity.ToTable("PersonalRecipe");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.NumberOfService).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Account).WithMany(p => p.PersonalRecipes)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Recipe).WithMany(p => p.PersonalRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId);

            entity.ToTable("Recipe");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CensorId).HasColumnName("CensorID");
            entity.Property(e => e.CreateById).HasColumnName("CreateByID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NumberOfService).HasDefaultValueSql("((0))");
            entity.Property(e => e.Price).HasDefaultValueSql("((0))");
            entity.Property(e => e.RecipeName).HasMaxLength(100);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Censor).WithMany(p => p.RecipeCensors)
                .HasForeignKey(d => d.CensorId);

            entity.HasOne(d => d.CreateBy).WithMany(p => p.RecipeCreateBies)
                .HasForeignKey(d => d.CreateById);           
                
        });

        modelBuilder.Entity<RecipeRate>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.AccountId });

            entity.ToTable("RecipeRate");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.AccountId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Account).WithMany(p => p.RecipeRates)
                .HasForeignKey(d => d.AccountId);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeRates)
                .HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<RecipeTransaction>(entity =>
        {
            entity.HasKey(e => e.RecipeTransactionId);

            entity.ToTable("RecipeTransaction");

            entity.Property(e => e.RecipeTransactionId).HasColumnName("RecipeTransactionID");
            entity.Property(e => e.CoinFluctuations).HasDefaultValueSql("((0))");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Detail).HasMaxLength(500);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

            entity.HasOne(d => d.Customer).WithMany(p => p.RecipeTransactions)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeTransactions)
                .HasForeignKey(d => d.RecipeId);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<ServiceFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedBackId);

            entity.ToTable("Service_Feedback");

            entity.Property(e => e.FeedBackId).HasColumnName("FeedBackID");
            entity.Property(e => e.Content).HasMaxLength(225);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.ServiceFeedbackCustomers)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Employee).WithMany(p => p.ServiceFeedbackEmployees)
                .HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId);

            entity.ToTable("Tag");

            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<RecipeTag>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.TagId });

            entity.ToTable("RecipeTag");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeTags).HasForeignKey(d => d.RecipeId);

            entity.HasOne(d => d.Tag).WithMany(p => p.RecipeTags).HasForeignKey(d => d.TagId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
