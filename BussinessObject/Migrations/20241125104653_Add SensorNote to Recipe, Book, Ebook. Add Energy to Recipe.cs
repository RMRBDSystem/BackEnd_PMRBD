using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class AddSensorNotetoRecipeBookEbookAddEnergytoRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Energy",
                table: "Recipe",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SensorNote",
                table: "Recipe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SensorNote",
                table: "EBook",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SensorNote",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Energy",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "SensorNote",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "SensorNote",
                table: "EBook");

            migrationBuilder.DropColumn(
                name: "SensorNote",
                table: "Book");
        }
    }
}
