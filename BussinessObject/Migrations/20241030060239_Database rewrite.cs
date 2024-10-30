using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class Databaserewrite : Migration
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
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookCate__19093A2BE1875784", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE3A8E1D1BE3", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tag__657CF9ACC8C9F18C", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    GoogleID = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Coin = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    AccountStatus = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__349DA586628C74C8", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK__Account__RoleID__412EB0B6",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "AccountProfile",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    FrontIDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BackIDCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Portrait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    IDCardNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CensorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AccountP__349DA58673F25A35", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK__AccountPr__Accou__44FF419A",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__AccountPr__Censo__45F365D3",
                        column: x => x.CensorID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "CoinTransaction",
                columns: table => new
                {
                    CoinTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    MoneyFluctuations = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CoinFluctuations = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CoinTran__610ECDE83A70CF87", x => x.CoinTransactionID);
                    table.ForeignKey(
                        name: "FK__CoinTrans__Custo__6FE99F9F",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "EBook",
                columns: table => new
                {
                    EBookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EBookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PDFUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateByID = table.Column<int>(type: "int", nullable: true),
                    CensorID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EBook__7C0B75DE78A5ACEF", x => x.EBookID);
                    table.ForeignKey(
                        name: "FK__EBook__CategoryI__797309D9",
                        column: x => x.CategoryID,
                        principalTable: "BookCategory",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__EBook__CensorID__787EE5A0",
                        column: x => x.CensorID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__EBook__CreateByI__778AC167",
                        column: x => x.CreateByID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E32A078B7C7", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__Accou__52593CB8",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "PhoneNumber",
                columns: table => new
                {
                    PhoneNumberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PhoneNum__D2D34FB1D7AE8963", x => x.PhoneNumberID);
                    table.ForeignKey(
                        name: "FK__PhoneNumb__Accou__49C3F6B7",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfService = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Nutrition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tutorial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateByID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CensorID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TotalTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Recipe__FDD988D0A98C78E3", x => x.RecipeID);
                    table.ForeignKey(
                        name: "FK__Recipe__CensorID__5812160E",
                        column: x => x.CensorID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__Recipe__CreateBy__5629CD9C",
                        column: x => x.CreateByID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Service_Feedback",
                columns: table => new
                {
                    FeedBackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Service___E2CB3867CB8D4277", x => x.FeedBackID);
                    table.ForeignKey(
                        name: "FK__Service_F__Custo__6383C8BA",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__Service_F__Emplo__628FA481",
                        column: x => x.EmployeeID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "BookShelf",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    EBookID = table.Column<int>(type: "int", nullable: false),
                    RatePoint = table.Column<int>(type: "int", nullable: true),
                    PurchasePrice = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookShel__536ED3E52AC1BCE2", x => new { x.CustomerID, x.EBookID });
                    table.ForeignKey(
                        name: "FK__BookShelf__Custo__7C4F7684",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__BookShelf__EBook__7D439ABD",
                        column: x => x.EBookID,
                        principalTable: "EBook",
                        principalColumn: "EBookID");
                });

            migrationBuilder.CreateTable(
                name: "EBookTransaction",
                columns: table => new
                {
                    EBookTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    EBookID = table.Column<int>(type: "int", nullable: true),
                    CoinFluctuations = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EBookTra__1A9A10901E1B7544", x => x.EBookTransactionID);
                    table.ForeignKey(
                        name: "FK__EBookTran__Custo__02084FDA",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__EBookTran__EBook__02FC7413",
                        column: x => x.EBookID,
                        principalTable: "EBook",
                        principalColumn: "EBookID");
                });

            migrationBuilder.CreateTable(
                name: "CustomerAddress",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(type: "int", nullable: true),
                    AddressStatus = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    Ward_Code = table.Column<int>(type: "int", nullable: false),
                    District_Code = table.Column<int>(type: "int", nullable: false),
                    Province_Code = table.Column<int>(type: "int", nullable: false),
                    AddressDetail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PhoneNumberID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Customer__091C2A1B175C7F51", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK__CustomerA__Accou__4D94879B",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__CustomerA__Phone__4F7CD00D",
                        column: x => x.PhoneNumberID,
                        principalTable: "PhoneNumber",
                        principalColumn: "PhoneNumberID");
                });

            migrationBuilder.CreateTable(
                name: "AccountRecipe",
                columns: table => new
                {
                    AccountsAccountId = table.Column<int>(type: "int", nullable: false),
                    RecipesRecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRecipe", x => new { x.AccountsAccountId, x.RecipesRecipeId });
                    table.ForeignKey(
                        name: "FK_AccountRecipe_Account_AccountsAccountId",
                        column: x => x.AccountsAccountId,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRecipe_Recipe_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalRecipe",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    NumberOfService = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Nutrition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tutorial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ingredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasePrice = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Personal__67936E9BFA8C1025", x => new { x.RecipeID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK__PersonalR__Custo__6754599E",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__PersonalR__Recip__66603565",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "RecipeRate",
                columns: table => new
                {
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    RatePoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeRate", x => new { x.RecipeID, x.CustomerID });
                    table.ForeignKey(
                        name: "FK_RecipeRate_Account_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeRate_Recipe_RecipeID",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipeId, x.TagId });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTransaction",
                columns: table => new
                {
                    RecipeTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RecipeID = table.Column<int>(type: "int", nullable: true),
                    CoinFluctuations = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RecipeTr__AD09EAF84D449249", x => x.RecipeTransactionID);
                    table.ForeignKey(
                        name: "FK__RecipeTra__Custo__6B24EA82",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__RecipeTra__Recip__6C190EBB",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    UnitInStock = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CensorID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateByID = table.Column<int>(type: "int", nullable: true),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    Required_Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SenderAddressID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Book__3DE0C227629A9559", x => x.BookID);
                    table.ForeignKey(
                        name: "FK__Book__CategoryID__09A971A2",
                        column: x => x.CategoryID,
                        principalTable: "BookCategory",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK__Book__CensorID__07C12930",
                        column: x => x.CensorID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__Book__CreateByID__08B54D69",
                        column: x => x.CreateByID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__Book__SenderAddr__0A9D95DB",
                        column: x => x.SenderAddressID,
                        principalTable: "CustomerAddress",
                        principalColumn: "AddressID");
                });

            migrationBuilder.CreateTable(
                name: "BookOrder",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    BookID = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ShipFee = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    PurchaseMethod = table.Column<int>(type: "int", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    Order_Code = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    ClientAddressID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookOrde__C3905BAFEEF8B964", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK__BookOrder__BookI__160F4887",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__BookOrder__Clien__17F790F9",
                        column: x => x.ClientAddressID,
                        principalTable: "CustomerAddress",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK__BookOrder__Custo__151B244E",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "BookRate",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    RatePoint = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookRate__C770689A26ADE04F", x => new { x.CustomerID, x.BookID });
                    table.ForeignKey(
                        name: "FK__BookRate__BookID__1BC821DD",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__BookRate__Custom__1AD3FDA4",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RootCommentID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))"),
                    BookID = table.Column<int>(type: "int", nullable: true),
                    EBookID = table.Column<int>(type: "int", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RecipeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment__C3B4DFAA801BC416", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK__Comment__BookID__282DF8C2",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__Comment__Custome__2A164134",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__Comment__EBookID__29221CFB",
                        column: x => x.EBookID,
                        principalTable: "EBook",
                        principalColumn: "EBookID");
                    table.ForeignKey(
                        name: "FK__Comment__RecipeI__2B0A656D",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                    table.ForeignKey(
                        name: "FK__Comment__RootCom__2645B050",
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
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Image__7516F4EC1AE18B0A", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK__Image__BookID__0E6E26BF",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__Image__RecipeID__0D7A0286",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID");
                });

            migrationBuilder.CreateTable(
                name: "BookOrderStatus",
                columns: table => new
                {
                    BookOrderStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookOrde__967212521220FF6E", x => x.BookOrderStatusID);
                    table.ForeignKey(
                        name: "FK__BookOrder__Order__1EA48E88",
                        column: x => x.OrderID,
                        principalTable: "BookOrder",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "BookTransaction",
                columns: table => new
                {
                    BookTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    MoneyFluctuations = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CoinFluctuations = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookTran__4F2B2B26F32064C6", x => x.BookTransactionID);
                    table.ForeignKey(
                        name: "FK__BookTrans__Custo__2180FB33",
                        column: x => x.CustomerID,
                        principalTable: "Account",
                        principalColumn: "AccountID");
                    table.ForeignKey(
                        name: "FK__BookTrans__Order__22751F6C",
                        column: x => x.OrderID,
                        principalTable: "BookOrder",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleID",
                table: "Account",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "UQ__Account__A6FBF31B9019A517",
                table: "Account",
                column: "GoogleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Account__A9D1053417B0D0A4",
                table: "Account",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfile_CensorID",
                table: "AccountProfile",
                column: "CensorID");

            migrationBuilder.CreateIndex(
                name: "UQ__AccountP__2CEB98360078DE47",
                table: "AccountProfile",
                column: "IDCardNumber",
                unique: true,
                filter: "[IDCardNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRecipe_RecipesRecipeId",
                table: "AccountRecipe",
                column: "RecipesRecipeId");

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
                name: "IX_Book_SenderAddressID",
                table: "Book",
                column: "SenderAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrder_BookID",
                table: "BookOrder",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrder_ClientAddressID",
                table: "BookOrder",
                column: "ClientAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrder_CustomerID",
                table: "BookOrder",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrderStatus_OrderID",
                table: "BookOrderStatus",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_BookRate_BookID",
                table: "BookRate",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookShelf_EBookID",
                table: "BookShelf",
                column: "EBookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransaction_CustomerID",
                table: "BookTransaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransaction_OrderID",
                table: "BookTransaction",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_CoinTransaction_CustomerID",
                table: "CoinTransaction",
                column: "CustomerID");

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
                name: "IX_CustomerAddress_AccountID",
                table: "CustomerAddress",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_PhoneNumberID",
                table: "CustomerAddress",
                column: "PhoneNumberID");

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
                name: "IX_EBookTransaction_CustomerID",
                table: "EBookTransaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_EBookTransaction_EBookID",
                table: "EBookTransaction",
                column: "EBookID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_BookID",
                table: "Image",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Image_RecipeID",
                table: "Image",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_AccountID",
                table: "Notification",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalRecipe_CustomerID",
                table: "PersonalRecipe",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumber_AccountID",
                table: "PhoneNumber",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "UQ__PhoneNum__85FB4E3847CDA52B",
                table: "PhoneNumber",
                column: "Number",
                unique: true,
                filter: "[Number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CensorID",
                table: "Recipe",
                column: "CensorID");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_CreateByID",
                table: "Recipe",
                column: "CreateByID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeRate_CustomerID",
                table: "RecipeRate",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagId",
                table: "RecipeTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTransaction_CustomerID",
                table: "RecipeTransaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTransaction_RecipeID",
                table: "RecipeTransaction",
                column: "RecipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Feedback_CustomerID",
                table: "Service_Feedback",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Feedback_EmployeeID",
                table: "Service_Feedback",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountProfile");

            migrationBuilder.DropTable(
                name: "AccountRecipe");

            migrationBuilder.DropTable(
                name: "BookOrderStatus");

            migrationBuilder.DropTable(
                name: "BookRate");

            migrationBuilder.DropTable(
                name: "BookShelf");

            migrationBuilder.DropTable(
                name: "BookTransaction");

            migrationBuilder.DropTable(
                name: "CoinTransaction");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "EBookTransaction");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PersonalRecipe");

            migrationBuilder.DropTable(
                name: "RecipeRate");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "RecipeTransaction");

            migrationBuilder.DropTable(
                name: "Service_Feedback");

            migrationBuilder.DropTable(
                name: "BookOrder");

            migrationBuilder.DropTable(
                name: "EBook");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
