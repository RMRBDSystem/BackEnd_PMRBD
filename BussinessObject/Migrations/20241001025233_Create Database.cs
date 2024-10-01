using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    EmployeeTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.EmployeeTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FrontIDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackIDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Portrait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDCardNumBer = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    EmployeeTypeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK__Employee__Employ__5165187F",
                        column: x => x.EmployeeTypeID,
                        principalTable: "EmployeeType",
                        principalColumn: "EmployeeTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coin = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    AccountStatus = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    FrontIDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackIDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Portrait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDCardNumber = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    SellerStatus = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    CensorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK__Customer__Censor__5812160E",
                        column: x => x.CensorID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    UnitInStock = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CensorID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateByID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    ISBN = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookID);
                    table.ForeignKey(
                        name: "FK__Book__CategoryID__03F0984C",
                        column: x => x.CategoryID,
                        principalTable: "BookCategory",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__Book__CensorID__02084FDA",
                        column: x => x.CensorID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK__Book__CreateByID__02FC7413",
                        column: x => x.CreateByID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "BookOrder",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    PurchaseMethod = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrder", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__BookOrder__Custo__0B91BA14",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "EBook",
                columns: table => new
                {
                    EBookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EBookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PDFUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CensorID = table.Column<int>(type: "int", nullable: true),
                    CreateByID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EBook", x => x.EBookID);
                    table.ForeignKey(
                        name: "FK__EBook__CategoryI__7A672E12",
                        column: x => x.CategoryID,
                        principalTable: "BookCategory",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__EBook__CensorID__787EE5A0",
                        column: x => x.CensorID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK__EBook__CreateByI__797309D9",
                        column: x => x.CreateByID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__Custo__6EF57B66",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Notificat__Emplo__6E01572D",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberofService = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    Nutrition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tutorial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateByID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((-1))"),
                    CensorID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "date", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: true),
                    TotalTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK__Recipe__CensorID__6383C8BA",
                        column: x => x.CensorID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK__Recipe__CreateBy__60A75C0F",
                        column: x => x.CreateByID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "ServiceFeedBack",
                columns: table => new
                {
                    FeedBackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceFeedBack", x => x.FeedBackID);
                    table.ForeignKey(
                        name: "FK__ServiceFe__Custo__5CD6CB2B",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__ServiceFe__Emplo__5BE2A6F2",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "BookRate",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    RatePoint = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRate", x => new { x.CustomerID, x.BookID });
                    table.ForeignKey(
                        name: "FK__BookRate__BookID__08B54D69",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__BookRate__Custom__07C12930",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "BookOrderDetail",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrderDetail", x => new { x.OrderID, x.BookID });
                    table.ForeignKey(
                        name: "FK__BookOrder__BookI__0F624AF8",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__BookOrder__Order__0E6E26BF",
                        column: x => x.OrderID,
                        principalTable: "BookOrder",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "BookShelf",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    EBookID = table.Column<int>(type: "int", nullable: false),
                    RatePoint = table.Column<int>(type: "int", nullable: true),
                    PurchasePrice = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShelf", x => new { x.CustomerID, x.EBookID });
                    table.ForeignKey(
                        name: "FK__BookShelf__Custo__7D439ABD",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__BookShelf__EBook__7E37BEF6",
                        column: x => x.EBookID,
                        principalTable: "EBook",
                        principalColumn: "EBookID");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RootCommentID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    BookID = table.Column<int>(type: "int", nullable: true),
                    EBookID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RecipeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK__Comment__BookID__1332DBDC",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__Comment__Custome__151B244E",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Comment__EBookID__14270015",
                        column: x => x.EBookID,
                        principalTable: "EBook",
                        principalColumn: "EBookID");
                    table.ForeignKey(
                        name: "FK__Comment__RecipeI__160F4887",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                    table.ForeignKey(
                        name: "FK__Comment__RootCom__123EB7A3",
                        column: x => x.RootCommentID,
                        principalTable: "Comment",
                        principalColumn: "CommentID");
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeID = table.Column<int>(type: "int", nullable: true),
                    BookID = table.Column<int>(type: "int", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK__Image__BookID__19DFD96B",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__Image__RecipeID__18EBB532",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "PersonalRecipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    NumberofService = table.Column<int>(type: "int", nullable: true),
                    Nutrition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tutorial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PurchasePrice = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalRecipe", x => new { x.RecipeID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK__PersonalR__Custo__72C60C4A",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__PersonalR__Recip__71D1E811",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "Recipe_Rate",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    RatePoint = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe_Rate", x => new { x.RecipeID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK__Recipe_Ra__Custo__6B24EA82",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Recipe_Ra__Recip__6A30C649",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipeID, x.TagID });
                    table.ForeignKey(
                        name: "FK__RecipeTag__Recip__66603565",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                    table.ForeignKey(
                        name: "FK__RecipeTag__TagID__6754599E",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RecipeID = table.Column<int>(type: "int", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    EBookID = table.Column<int>(type: "int", nullable: true),
                    MoneyFluctuations = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CoinFluctuations = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionID);
                    table.ForeignKey(
                        name: "FK__Transacti__Custo__1CBC4616",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Transacti__EBook__1F98B2C1",
                        column: x => x.EBookID,
                        principalTable: "EBook",
                        principalColumn: "EBookID");
                    table.ForeignKey(
                        name: "FK__Transacti__Order__1EA48E88",
                        column: x => x.OrderID,
                        principalTable: "BookOrder",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK__Transacti__Recip__1DB06A4F",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_CategoryID",
                table: "Book",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CensorID",
                table: "Book",
                column: "CensorID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_CreateByID",
                table: "Book",
                column: "CreateByID");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrder_CustomerID",
                table: "BookOrder",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrderDetail_BookID",
                table: "BookOrderDetail",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookRate_BookID",
                table: "BookRate",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookShelf_EBookID",
                table: "BookShelf",
                column: "EBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_BookID",
                table: "Comment",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CustomerID",
                table: "Comment",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_EBookID",
                table: "Comment",
                column: "EBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_RecipeID",
                table: "Comment",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_RootCommentID",
                table: "Comment",
                column: "RootCommentID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CensorID",
                table: "Customer",
                column: "CensorID");

            migrationBuilder.CreateIndex(
                name: "UQ__Customer__A9D10534F0D4AE3A",
                table: "Customer",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EBook_CategoryID",
                table: "EBook",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_EBook_CensorID",
                table: "EBook",
                column: "CensorID");

            migrationBuilder.CreateIndex(
                name: "IX_EBook_CreateByID",
                table: "EBook",
                column: "CreateByID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_EmployeeTypeID",
                table: "Employee",
                column: "EmployeeTypeID");

            migrationBuilder.CreateIndex(
                name: "UQ__Employee__A9D10534EB410196",
                table: "Employee",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Image_BookID",
                table: "Image",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_RecipeID",
                table: "Image",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CustomerID",
                table: "Notification",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_EmployeeID",
                table: "Notification",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecipe_CustomerID",
                table: "PersonalRecipe",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CensorID",
                table: "Recipe",
                column: "CensorID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CreateByID",
                table: "Recipe",
                column: "CreateByID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_Rate_CustomerID",
                table: "Recipe_Rate",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagID",
                table: "RecipeTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeedBack_CustomerID",
                table: "ServiceFeedBack",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceFeedBack_EmployeeID",
                table: "ServiceFeedBack",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerID",
                table: "Transaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_EBookID",
                table: "Transaction",
                column: "EBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OrderID",
                table: "Transaction",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_RecipeID",
                table: "Transaction",
                column: "RecipeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookOrderDetail");

            migrationBuilder.DropTable(
                name: "BookRate");

            migrationBuilder.DropTable(
                name: "BookShelf");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PersonalRecipe");

            migrationBuilder.DropTable(
                name: "Recipe_Rate");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "ServiceFeedBack");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "EBook");

            migrationBuilder.DropTable(
                name: "BookOrder");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "EmployeeType");
        }
    }
}
