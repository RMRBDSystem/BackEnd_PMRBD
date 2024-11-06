using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class fixWardCodetype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {          
            migrationBuilder.DropColumn(
                name: "PhoneNumberId",
                table: "CustomerAddress");         

            migrationBuilder.AlterColumn<string>(
                name: "Ward_Code",
                table: "CustomerAddress",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
            migrationBuilder.AlterColumn<int>(
                name: "Ward_Code",
                table: "CustomerAddress",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumberId",
                table: "CustomerAddress",
                type: "int",
                nullable: true);            
        }
    }
}
