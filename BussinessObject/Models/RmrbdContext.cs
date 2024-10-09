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

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookCategory> BookCategories { get; set; }

    public virtual DbSet<BookOrder> BookOrders { get; set; }

    public virtual DbSet<BookRate> BookRates { get; set; }

    public virtual DbSet<BookShelf> BookShelves { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Ebook> Ebooks { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PersonalRecipe> PersonalRecipes { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeRate> RecipeRates { get; set; }

    public virtual DbSet<ServiceFeedBack> ServiceFeedBacks { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<RecipeTransaction> RecipeTransactions { get; set; }

    public virtual DbSet<EbookTransaction> EbookTransactions { get; set; }

    public virtual DbSet<BookTransaction> BookTransactions { get; set; }
    
    public virtual DbSet<BookOrderStatus> BookOrderStatus { get; set; }

    public virtual DbSet<CoinTransaction> CoinTransactions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
                .HasDefaultValueSql("((0))")
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Book__CategoryID__03F0984C");

            entity.HasOne(d => d.Censor).WithMany(p => p.Books)
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__Book__CensorID__02084FDA");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.Books)
                .HasForeignKey(d => d.CreateById)
                .HasConstraintName("FK__Book__CreateByID__02FC7413");
        });

        modelBuilder.Entity<BookCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.ToTable("BookCategory");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<BookOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("BookOrder");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Book).WithMany(p => p.BookOrder)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookOrder__BookI__0F624AF8");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BookOrder__Custo__0B91BA14");
        });

        modelBuilder.Entity<BookRate>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.BookId });

            entity.ToTable("BookRate");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.BookId).HasColumnName("BookID");

            entity.HasOne(d => d.Book).WithMany(p => p.BookRates)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookRate__BookID__08B54D69");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookRates)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookRate__Custom__07C12930");
        });

        modelBuilder.Entity<BookShelf>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.EbookId });

            entity.ToTable("BookShelf");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookShelves)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookShelf__Custo__7D439ABD");

            entity.HasOne(d => d.Ebook).WithMany(p => p.BookShelves)
                .HasForeignKey(d => d.EbookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookShelf__EBook__7E37BEF6");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId);

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.RootCommentId).HasColumnName("RootCommentID");

            entity.HasOne(d => d.Book).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Comment__BookID__1332DBDC");

            entity.HasOne(d => d.Customer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Comment__Custome__151B244E");

            entity.HasOne(d => d.Ebook).WithMany(p => p.Comments)
                .HasForeignKey(d => d.EbookId)
                .HasConstraintName("FK__Comment__EBookID__14270015");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Comment__RecipeI__160F4887");

            entity.HasOne(d => d.RootComment).WithMany(p => p.InverseRootComment)
                .HasForeignKey(d => d.RootCommentId)
                .HasConstraintName("FK__Comment__RootCom__123EB7A3");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.GoogleId).IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.AccountStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.BackIdcard).HasColumnName("BackIDCard");
            entity.Property(e => e.CensorId).HasColumnName("CensorID");
            entity.Property(e => e.Coin).HasDefaultValueSql("((0))");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.GoogleId).HasMaxLength(100);
            entity.Property(e => e.FrontIdcard).HasColumnName("FrontIDCard");
            entity.Property(e => e.IdcardNumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("IDCardNumber");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.SellerStatus).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Censor).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__Customer__Censor__5812160E");
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

            entity.HasOne(d => d.Category).WithMany(p => p.Ebooks)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__EBook__CategoryI__7A672E12");

            entity.HasOne(d => d.Censor).WithMany(p => p.Ebooks)
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__EBook__CensorID__787EE5A0");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.Ebooks)
                .HasForeignKey(d => d.CreateById)
                .HasConstraintName("FK__EBook__CreateByI__797309D9");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("Employee");

            entity.HasIndex(e => e.Email, "UQ__Employee__A9D10534EB410196").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.BackIdcard).HasColumnName("BackIDCard");
            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeTypeId).HasColumnName("EmployeeTypeID");
            entity.Property(e => e.FrontIdcard).HasColumnName("FrontIDCard");
            entity.Property(e => e.IdcardNumBer)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("IDCardNumBer");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.EmployeeType).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeTypeId)
                .HasConstraintName("FK__Employee__Employ__5165187F");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(e => e.EmployeeTypeId);

            entity.ToTable("EmployeeType");

            entity.Property(e => e.EmployeeTypeId).HasColumnName("EmployeeTypeID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId);

            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

            entity.HasOne(d => d.Book).WithMany(p => p.Images)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Image__BookID__19DFD96B");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Images)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Image__RecipeID__18EBB532");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId);

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Notificat__Custo__6EF57B66");

            entity.HasOne(d => d.Employee).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Notificat__Emplo__6E01572D");
        });

        modelBuilder.Entity<PersonalRecipe>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.CustomerId });

            entity.ToTable("PersonalRecipe");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.PersonalRecipes)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonalR__Custo__72C60C4A");

            entity.HasOne(d => d.Recipe).WithMany(p => p.PersonalRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonalR__Recip__71D1E811");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId);

            entity.ToTable("Recipe");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CensorId).HasColumnName("CensorID");
            entity.Property(e => e.CreateById).HasColumnName("CreateByID");
            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.NumberofService).HasDefaultValueSql("((1))");
            entity.Property(e => e.Price).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((-1))");
            entity.Property(e => e.UpdateDate).HasColumnType("date");

            entity.HasOne(d => d.Censor).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__Recipe__CensorID__6383C8BA");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CreateById)
                .HasConstraintName("FK__Recipe__CreateBy__60A75C0F");

            entity.HasMany(d => d.Tags).WithMany(p => p.Recipes)
                .UsingEntity<Dictionary<string, object>>(
                    "RecipeTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RecipeTag__TagID__6754599E"),
                    l => l.HasOne<Recipe>().WithMany()
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RecipeTag__Recip__66603565"),
                    j =>
                    {
                        j.HasKey("RecipeId", "TagId");
                        j.ToTable("RecipeTag");
                        j.IndexerProperty<int>("RecipeId").HasColumnName("RecipeID");
                        j.IndexerProperty<int>("TagId").HasColumnName("TagID");
                    });
        });

        modelBuilder.Entity<RecipeRate>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.CustomerId });

            entity.ToTable("Recipe_Rate");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

            entity.HasOne(d => d.Customer).WithMany(p => p.RecipeRates)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipe_Ra__Custo__6B24EA82");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeRates)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipe_Ra__Recip__6A30C649");
        });

        modelBuilder.Entity<ServiceFeedBack>(entity =>
        {
            entity.HasKey(e => e.FeedBackId);

            entity.ToTable("ServiceFeedBack");

            entity.Property(e => e.FeedBackId).HasColumnName("FeedBackID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.ServiceFeedBacks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__ServiceFe__Custo__5CD6CB2B");

            entity.HasOne(d => d.Employee).WithMany(p => p.ServiceFeedBacks)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__ServiceFe__Emplo__5BE2A6F2");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId);

            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("TagID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<RecipeTransaction>(entity =>
        {
            entity.HasKey(e => e.RecipeTransactionId);

            entity.ToTable("RecipeTransaction");

            entity.Property(e => e.RecipeTransactionId).HasColumnName("RecipeTransactionID");
            entity.Property(e => e.CoinFluctuations).HasColumnType("int");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

            entity.HasOne(d => d.Customer).WithMany(p => p.RecipeTransactions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Transacti__Custo__1CBC4616");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeTransactions)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Transacti__Recip__1DB06A4F");
        });

        modelBuilder.Entity<EbookTransaction>(entity =>
        {
            entity.HasKey(e => e.EbookTransactionId);

            entity.ToTable("EbookTransaction");

            entity.Property(e => e.EbookTransactionId).HasColumnName("EbookTransactionID");
            entity.Property(e => e.CoinFluctuations).HasColumnType("int");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");

            entity.HasOne(d => d.Customer).WithMany(p => p.EbookTransactions)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Ebook).WithMany(p => p.EbookTransactions)
                .HasForeignKey(d => d.EbookId);
        });

        modelBuilder.Entity<BookTransaction>(entity =>
        {
            entity.HasKey(e => e.BookTransactionId);

            entity.ToTable("BookTransaction");

            entity.Property(e => e.BookTransactionId).HasColumnName("BookTransactionID");
            entity.Property(e => e.MoneyFluctuations).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.CoinFluctuations).HasColumnType("int");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.BookOrderId).HasColumnName("BookOrderID");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookTransactions)
                .HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.BookOrder).WithMany(p => p.BookTransactions)
                .HasForeignKey(d => d.BookOrderId);
        });

        modelBuilder.Entity<BookOrderStatus>(entity =>
        {
            entity.HasKey(e => e.BookOrderStatusId);
            entity.ToTable("BookOrderStatus");

            entity.Property(e => e.BookOrderStatusId).HasColumnName("BookOrderStatusID");

            entity.HasOne(d => d.BookOrder).WithMany(p => p.BookOrderStatuses)
            .HasForeignKey(d => d.BookOrderStatusId);
        });

        modelBuilder.Entity<CoinTransaction>(entity =>
        {
            entity.HasKey(e => e.CoinTransactionId);
            entity.ToTable("CoinTransaction");

            entity.Property(e => e.CoinTransactionId).HasColumnName("CoinTransactionID");

            entity.HasOne(d => d.Customer).WithMany(p => p.CoinTransactions).
                HasForeignKey(d => d.CustomerId);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
