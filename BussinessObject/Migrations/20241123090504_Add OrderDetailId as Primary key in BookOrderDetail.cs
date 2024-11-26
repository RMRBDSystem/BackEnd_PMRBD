using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderDetailIdasPrimarykeyinBookOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookOrderDetail",
                table: "BookOrderDetail");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId",
                table: "BookOrderDetail",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookOrderDetail",
                table: "BookOrderDetail",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_BookOrderDetail_OrderId",
                table: "BookOrderDetail",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookOrderDetail",
                table: "BookOrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_BookOrderDetail_OrderId",
                table: "BookOrderDetail");

            migrationBuilder.DropColumn(
                name: "OrderDetailId",
                table: "BookOrderDetail");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookOrderDetail",
                table: "BookOrderDetail",
                columns: new[] { "OrderId", "BookId" });
        }
    }
}
