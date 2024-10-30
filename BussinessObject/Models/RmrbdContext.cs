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

    public virtual DbSet<Account> Accounts { get; set; }

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

    public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }

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
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA586628C74C8");

            entity.ToTable("Account");

            entity.HasIndex(e => e.GoogleId, "UQ__Account__A6FBF31B9019A517").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Account__A9D1053417B0D0A4").IsUnique();

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
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Account__RoleID__412EB0B6");
        });

        modelBuilder.Entity<AccountProfile>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__AccountP__349DA58673F25A35");

            entity.ToTable("AccountProfile");

            entity.HasIndex(e => e.IdcardNumber, "UQ__AccountP__2CEB98360078DE47").IsUnique();

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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountPr__Accou__44FF419A");

            entity.HasOne(d => d.Censor).WithMany(p => p.AccountProfileCensors)
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__AccountPr__Censo__45F365D3");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Book__3DE0C227629A9559");

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
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Book__CategoryID__09A971A2");

            entity.HasOne(d => d.Censor).WithMany(p => p.BookCensors)
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__Book__CensorID__07C12930");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.BookCreateBies)
                .HasForeignKey(d => d.CreateById)
                .HasConstraintName("FK__Book__CreateByID__08B54D69");

            entity.HasOne(d => d.SenderAddress).WithMany(p => p.Books)
                .HasForeignKey(d => d.SenderAddressId)
                .HasConstraintName("FK__Book__SenderAddr__0A9D95DB");
        });

        modelBuilder.Entity<BookCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__BookCate__19093A2BE1875784");

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
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__BookOrder__BookI__160F4887");

            entity.HasOne(d => d.ClientAddress).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.ClientAddressId)
                .HasConstraintName("FK__BookOrder__Clien__17F790F9");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BookOrder__Custo__151B244E");
        });

        modelBuilder.Entity<BookOrderStatus>(entity =>
        {
            entity.HasKey(e => e.BookOrderStatusId).HasName("PK__BookOrde__967212521220FF6E");

            entity.ToTable("BookOrderStatus");

            entity.Property(e => e.BookOrderStatusId).HasColumnName("BookOrderStatusID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.StatusDate).HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.BookOrderStatuses)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__BookOrder__Order__1EA48E88");
        });

        modelBuilder.Entity<BookRate>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.BookId }).HasName("PK__BookRate__C770689A26ADE04F");

            entity.ToTable("BookRate");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.BookId).HasColumnName("BookID");

            entity.HasOne(d => d.Book).WithMany(p => p.BookRates)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookRate__BookID__1BC821DD");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookRates)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookRate__Custom__1AD3FDA4");
        });

        modelBuilder.Entity<BookShelf>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.EbookId }).HasName("PK__BookShel__536ED3E52AC1BCE2");

            entity.ToTable("BookShelf");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.PurchaseDate).HasColumnType("datetime");
            entity.Property(e => e.PurchasePrice).HasDefaultValueSql("((0))");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookShelves)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookShelf__Custo__7C4F7684");

            entity.HasOne(d => d.Ebook).WithMany(p => p.BookShelves)
                .HasForeignKey(d => d.EbookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookShelf__EBook__7D439ABD");
        });

        modelBuilder.Entity<BookTransaction>(entity =>
        {
            entity.HasKey(e => e.BookTransactionId).HasName("PK__BookTran__4F2B2B26F32064C6");

            entity.ToTable("BookTransaction");

            entity.Property(e => e.BookTransactionId).HasColumnName("BookTransactionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MoneyFluctuations).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.BookTransactions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__BookTrans__Custo__2180FB33");

            entity.HasOne(d => d.Order).WithMany(p => p.BookTransactions)
                .HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<CoinTransaction>(entity =>
        {
            entity.HasKey(e => e.CoinTransactionId).HasName("PK__CoinTran__610ECDE83A70CF87");

            entity.ToTable("CoinTransaction");

            entity.Property(e => e.CoinTransactionId).HasColumnName("CoinTransactionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.MoneyFluctuations).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.CoinTransactions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__CoinTrans__Custo__6FE99F9F");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAA801BC416");

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
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Comment__BookID__282DF8C2");

            entity.HasOne(d => d.Customer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Comment__Custome__2A164134");

            entity.HasOne(d => d.Ebook).WithMany(p => p.Comments)
                .HasForeignKey(d => d.EbookId)
                .HasConstraintName("FK__Comment__EBookID__29221CFB");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Comments)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Comment__RecipeI__2B0A656D");

            entity.HasOne(d => d.RootComment).WithMany(p => p.InverseRootComment)
                .HasForeignKey(d => d.RootCommentId)
                .HasConstraintName("FK__Comment__RootCom__2645B050");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Customer__091C2A1B175C7F51");

            entity.ToTable("CustomerAddress");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AddressDetail).HasMaxLength(500);
            entity.Property(e => e.AddressStatus).HasDefaultValueSql("((1))");
            entity.Property(e => e.DistrictCode).HasColumnName("District_Code");
            entity.Property(e => e.PhoneNumberId).HasColumnName("PhoneNumberID");
            entity.Property(e => e.ProvinceCode).HasColumnName("Province_Code");
            entity.Property(e => e.WardCode).HasColumnName("Ward_Code");

            entity.HasOne(d => d.Account).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__CustomerA__Accou__4D94879B");

            entity.HasOne(d => d.PhoneNumber).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.PhoneNumberId)
                .HasConstraintName("FK__CustomerA__Phone__4F7CD00D");
        });

        modelBuilder.Entity<Ebook>(entity =>
        {
            entity.HasKey(e => e.EbookId).HasName("PK__EBook__7C0B75DE78A5ACEF");

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
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__EBook__CategoryI__797309D9");

            entity.HasOne(d => d.Censor).WithMany(p => p.EbookCensors)
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__EBook__CensorID__787EE5A0");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.EbookCreateBies)
                .HasForeignKey(d => d.CreateById)
                .HasConstraintName("FK__EBook__CreateByI__778AC167");
        });

        modelBuilder.Entity<EbookTransaction>(entity =>
        {
            entity.HasKey(e => e.EbookTransactionId).HasName("PK__EBookTra__1A9A10901E1B7544");

            entity.ToTable("EBookTransaction");

            entity.Property(e => e.EbookTransactionId).HasColumnName("EBookTransactionID");
            entity.Property(e => e.CoinFluctuations).HasDefaultValueSql("((0))");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EbookId).HasColumnName("EBookID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Customer).WithMany(p => p.EbookTransactions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__EBookTran__Custo__02084FDA");

            entity.HasOne(d => d.Ebook).WithMany(p => p.EbookTransactions)
                .HasForeignKey(d => d.EbookId)
                .HasConstraintName("FK__EBookTran__EBook__02FC7413");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Image__7516F4EC1AE18B0A");

            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Book).WithMany(p => p.Images)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Image__BookID__0E6E26BF");

            entity.HasOne(d => d.Recipe).WithMany(p => p.Images)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__Image__RecipeID__0D7A0286");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E32A078B7C7");

            entity.ToTable("Notification");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Account).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Notificat__Accou__52593CB8");
        });

        modelBuilder.Entity<PersonalRecipe>(entity =>
        {
            entity.HasKey(e => new { e.RecipeId, e.CustomerId }).HasName("PK__Personal__67936E9BFA8C1025");

            entity.ToTable("PersonalRecipe");

            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.NumberOfService).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Account).WithMany(p => p.PersonalRecipes)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonalR__Custo__6754599E");

            entity.HasOne(d => d.Recipe).WithMany(p => p.PersonalRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PersonalR__Recip__66603565");
        });

        modelBuilder.Entity<PhoneNumber>(entity =>
        {
            entity.HasKey(e => e.PhoneNumberId).HasName("PK__PhoneNum__D2D34FB1D7AE8963");

            entity.ToTable("PhoneNumber");

            entity.HasIndex(e => e.Number, "UQ__PhoneNum__85FB4E3847CDA52B").IsUnique();

            entity.Property(e => e.PhoneNumberId).HasColumnName("PhoneNumberID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Number)
                .HasMaxLength(30)
                .HasColumnName("Number");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Account).WithMany(p => p.PhoneNumbers)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__PhoneNumb__Accou__49C3F6B7");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipe__FDD988D0A98C78E3");

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
                .HasForeignKey(d => d.CensorId)
                .HasConstraintName("FK__Recipe__CensorID__5812160E");

            entity.HasOne(d => d.CreateBy).WithMany(p => p.RecipeCreateBies)
                .HasForeignKey(d => d.CreateById)
                .HasConstraintName("FK__Recipe__CreateBy__5629CD9C");           
                
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
            entity.HasKey(e => e.RecipeTransactionId).HasName("PK__RecipeTr__AD09EAF84D449249");

            entity.ToTable("RecipeTransaction");

            entity.Property(e => e.RecipeTransactionId).HasColumnName("RecipeTransactionID");
            entity.Property(e => e.CoinFluctuations).HasDefaultValueSql("((0))");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Detail).HasMaxLength(500);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.RecipeId).HasColumnName("RecipeID");

            entity.HasOne(d => d.Customer).WithMany(p => p.RecipeTransactions)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__RecipeTra__Custo__6B24EA82");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeTransactions)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK__RecipeTra__Recip__6C190EBB");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A8E1D1BE3");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<ServiceFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedBackId).HasName("PK__Service___E2CB3867CB8D4277");

            entity.ToTable("Service_Feedback");

            entity.Property(e => e.FeedBackId).HasColumnName("FeedBackID");
            entity.Property(e => e.Content).HasMaxLength(225);
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.Customer).WithMany(p => p.ServiceFeedbackCustomers)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Service_F__Custo__6383C8BA");

            entity.HasOne(d => d.Employee).WithMany(p => p.ServiceFeedbackEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Service_F__Emplo__628FA481");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tag__657CF9ACC8C9F18C");

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
