using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class addwithdrawtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SensorNote",
                table: "Recipe",
                newName: "CensorNote");

            migrationBuilder.RenameColumn(
                name: "SensorNote",
                table: "EBook",
                newName: "CensorNote");

            migrationBuilder.RenameColumn(
                name: "SensorNote",
                table: "Book",
                newName: "CensorNote");

            migrationBuilder.CreateTable(
                name: "Withdraw",
                columns: table => new
                {
                    WithdrawId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Coin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Money = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Withdraw", x => x.WithdrawId);
                    table.ForeignKey(
                        name: "FK_Withdraw_Account_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Withdraw_AccountId",
                table: "Withdraw",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Withdraw");

            migrationBuilder.RenameColumn(
                name: "CensorNote",
                table: "Recipe",
                newName: "SensorNote");

            migrationBuilder.RenameColumn(
                name: "CensorNote",
                table: "EBook",
                newName: "SensorNote");

            migrationBuilder.RenameColumn(
                name: "CensorNote",
                table: "Book",
                newName: "SensorNote");
        }
    }
}
