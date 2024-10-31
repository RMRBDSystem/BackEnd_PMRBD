using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhoneNumberClassAddPhoneNumbertoCustomerAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__CustomerA__Phone__4F7CD00D",
                table: "CustomerAddress");

            migrationBuilder.DropTable(
                name: "PhoneNumber");

            migrationBuilder.DropIndex(
                name: "IX_CustomerAddress_PhoneNumberID",
                table: "CustomerAddress");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberID",
                table: "CustomerAddress",
                newName: "PhoneNumberId");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "CustomerAddress",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "CustomerAddress");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberId",
                table: "CustomerAddress",
                newName: "PhoneNumberID");

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

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAddress_PhoneNumberID",
                table: "CustomerAddress",
                column: "PhoneNumberID");

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

            migrationBuilder.AddForeignKey(
                name: "FK__CustomerA__Phone__4F7CD00D",
                table: "CustomerAddress",
                column: "PhoneNumberID",
                principalTable: "PhoneNumber",
                principalColumn: "PhoneNumberID");
        }
    }
}
