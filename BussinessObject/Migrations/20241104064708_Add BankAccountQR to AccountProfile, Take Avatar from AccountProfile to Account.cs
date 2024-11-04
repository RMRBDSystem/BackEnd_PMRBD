using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAccountQRtoAccountProfileTakeAvatarfromAccountProfiletoAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "AccountProfile",
                newName: "BankAccountQR");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Account",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "BankAccountQR",
                table: "AccountProfile",
                newName: "Avatar");
        }
    }
}
