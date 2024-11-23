using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddbookOrderDetaiB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_BookOrder_Book_BookID",
            //    table: "BookOrder");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "BookOrder");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "BookOrder",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookOrder_BookID",
                table: "BookOrder",
                newName: "IX_BookOrder_BookId");

            migrationBuilder.CreateTable(
                name: "BookOrderDetail",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOrderDetail", x => new { x.OrderId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookOrderDetail_BookOrder_OrderId",
                        column: x => x.OrderId,
                        principalTable: "BookOrder",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookOrderDetail_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookOrderDetail_BookId",
                table: "BookOrderDetail",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrder_Book_BookId",
                table: "BookOrder",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_BookOrder_Book_BookId",
            //    table: "BookOrder");

            migrationBuilder.DropTable(
                name: "BookOrderDetail");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookOrder",
                newName: "BookID");

            migrationBuilder.RenameIndex(
                name: "IX_BookOrder_BookId",
                table: "BookOrder",
                newName: "IX_BookOrder_BookID");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "BookOrder",
                type: "int",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrder_Book_BookID",
                table: "BookOrder",
                column: "BookID",
                principalTable: "Book",
                principalColumn: "BookID");
        }
    }
}
