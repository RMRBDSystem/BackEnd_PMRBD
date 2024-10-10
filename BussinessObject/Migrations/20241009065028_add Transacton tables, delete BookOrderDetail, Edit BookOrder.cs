using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class addTransactontablesdeleteBookOrderDetailEditBookOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__BookOrder__Custo__0B91BA14",
                table: "BookOrder");

            migrationBuilder.DropForeignKey(
                name: "FK__Employee__Employ__5165187F",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "BookOrderDetail");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "BookOrder");

            migrationBuilder.RenameIndex(
                name: "UQ__Customer__A9D10534F0D4AE3A",
                table: "Customer",
                newName: "IX_Customer_Email");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTypeID",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SellerStatus",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Customer",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Coin",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BookOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "BookOrder",
                type: "varchar(11)",
                unicode: false,
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldUnicode: false,
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "BookOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "BookOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "District_Id",
                table: "BookOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PaymentType",
                table: "BookOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BookOrder",
                type: "decimal(18,0)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BookOrder",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ShipFee",
                table: "BookOrder",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward_code",
                table: "BookOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District_code",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province_code",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Required_note",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward_code",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "height",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "length",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "weight",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "width",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BookOrderStatus",
                columns: table => new
                {
                    BookOrderStatusID = table.Column<int>(type: "int", nullable: false),
                    BookOrderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrderStatus", x => x.BookOrderStatusID);
                    table.ForeignKey(
                        name: "FK_BookOrderStatus_BookOrder_BookOrderStatusID",
                        column: x => x.BookOrderStatusID,
                        principalTable: "BookOrder",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTransaction",
                columns: table => new
                {
                    BookTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    BookOrderID = table.Column<int>(type: "int", nullable: true),
                    MoneyFluctuations = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    CoinFluctuations = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTransaction", x => x.BookTransactionID);
                    table.ForeignKey(
                        name: "FK_BookTransaction_BookOrder_BookOrderID",
                        column: x => x.BookOrderID,
                        principalTable: "BookOrder",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_BookTransaction_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                });

            migrationBuilder.CreateTable(
                name: "EbookTransaction",
                columns: table => new
                {
                    EbookTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    EBookID = table.Column<int>(type: "int", nullable: false),
                    CoinFluctuations = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EbookTransaction", x => x.EbookTransactionID);
                    table.ForeignKey(
                        name: "FK_EbookTransaction_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EbookTransaction_EBook_EBookID",
                        column: x => x.EBookID,
                        principalTable: "EBook",
                        principalColumn: "EBookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTransaction",
                columns: table => new
                {
                    RecipeTransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    RecipeID = table.Column<int>(type: "int", nullable: false),
                    CoinFluctuations = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTransaction", x => x.RecipeTransactionID);
                    table.ForeignKey(
                        name: "FK__Transacti__Custo__1CBC4616",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK__Transacti__Recip__1DB06A4F",
                        column: x => x.RecipeID,
                        principalTable: "Recipe",
                        principalColumn: "RecipeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customer_GoogleId",
                table: "Customer",
                column: "GoogleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookOrder_BookID",
                table: "BookOrder",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransaction_BookOrderID",
                table: "BookTransaction",
                column: "BookOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_BookTransaction_CustomerID",
                table: "BookTransaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_EbookTransaction_CustomerID",
                table: "EbookTransaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_EbookTransaction_EBookID",
                table: "EbookTransaction",
                column: "EBookID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTransaction_CustomerID",
                table: "RecipeTransaction",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTransaction_RecipeID",
                table: "RecipeTransaction",
                column: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK__BookOrder__BookI__0F624AF8",
                table: "BookOrder",
                column: "BookID",
                principalTable: "Book",
                principalColumn: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK__BookOrder__Custo__0B91BA14",
                table: "BookOrder",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Employee__Employ__5165187F",
                table: "Employee",
                column: "EmployeeTypeID",
                principalTable: "EmployeeType",
                principalColumn: "EmployeeTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__BookOrder__BookI__0F624AF8",
                table: "BookOrder");

            migrationBuilder.DropForeignKey(
                name: "FK__BookOrder__Custo__0B91BA14",
                table: "BookOrder");

            migrationBuilder.DropForeignKey(
                name: "FK__Employee__Employ__5165187F",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "BookOrderStatus");

            migrationBuilder.DropTable(
                name: "BookTransaction");

            migrationBuilder.DropTable(
                name: "EbookTransaction");

            migrationBuilder.DropTable(
                name: "RecipeTransaction");

            migrationBuilder.DropIndex(
                name: "IX_Customer_GoogleId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_BookOrder_BookID",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "District_Id",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "ShipFee",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "Ward_code",
                table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "District_code",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Province_code",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Required_note",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "Ward_code",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "height",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "length",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "width",
                table: "Book");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_Email",
                table: "Customer",
                newName: "UQ__Customer__A9D10534F0D4AE3A");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTypeID",
                table: "Employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SellerStatus",
                table: "Customer",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "Coin",
                table: "Customer",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BookOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "BookOrder",
                type: "varchar(11)",
                unicode: false,
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(11)",
                oldUnicode: false,
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "BookOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BookOrder",
                type: "nvarchar(max)",
                nullable: true);

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
                name: "Transaction",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    EBookID = table.Column<int>(type: "int", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    RecipeID = table.Column<int>(type: "int", nullable: true),
                    CoinFluctuations = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoneyFluctuations = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
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
                name: "IX_BookOrderDetail_BookID",
                table: "BookOrderDetail",
                column: "BookID");

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

            migrationBuilder.AddForeignKey(
                name: "FK__BookOrder__Custo__0B91BA14",
                table: "BookOrder",
                column: "CustomerID",
                principalTable: "Customer",
                principalColumn: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK__Employee__Employ__5165187F",
                table: "Employee",
                column: "EmployeeTypeID",
                principalTable: "EmployeeType",
                principalColumn: "EmployeeTypeID");
        }
    }
}
