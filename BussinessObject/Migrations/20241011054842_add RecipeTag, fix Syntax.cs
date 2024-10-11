using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    /// <inheritdoc />
    public partial class addRecipeTagfixSyntax : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookOrderStatus_BookOrder_BookOrderStatusID",
                table: "BookOrderStatus");

            migrationBuilder.DropForeignKey(
                name: "FK__RecipeTag__Recip__66603565",
                table: "RecipeTag");

            migrationBuilder.DropForeignKey(
                name: "FK__RecipeTag__TagID__6754599E",
                table: "RecipeTag");

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
                name: "Ward_code",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "TagID",
                table: "RecipeTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "RecipeID",
                table: "RecipeTag",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTag_TagID",
                table: "RecipeTag",
                newName: "IX_RecipeTag_TagId");

            migrationBuilder.RenameColumn(
                name: "NumberofService",
                table: "Recipe",
                newName: "NumberOfService");

            migrationBuilder.RenameColumn(
                name: "NumberofService",
                table: "PersonalRecipe",
                newName: "NumberOfService");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customer",
                newName: "Ward_code");

            migrationBuilder.RenameColumn(
                name: "BookOrderId",
                table: "BookOrderStatus",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "width",
                table: "Book",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "weight",
                table: "Book",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "length",
                table: "Book",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "Book",
                newName: "Height");

            migrationBuilder.AddColumn<string>(
                name: "Ingredient",
                table: "PersonalRecipe",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "District_code",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province_code",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShopAddress",
                table: "Customer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookOrderStatusID",
                table: "BookOrderStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Email",
                table: "Employee",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_GoogleId",
                table: "Employee",
                column: "GoogleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookOrderStatus_OrderId",
                table: "BookOrderStatus",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrderStatus_BookOrder_OrderId",
                table: "BookOrderStatus",
                column: "OrderId",
                principalTable: "BookOrder",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Recipe_RecipeId",
                table: "RecipeTag",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "RecipeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeTag_Tag_TagId",
                table: "RecipeTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "TagID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookOrderStatus_BookOrder_OrderId",
                table: "BookOrderStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Recipe_RecipeId",
                table: "RecipeTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeTag_Tag_TagId",
                table: "RecipeTag");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Email",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_GoogleId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_BookOrderStatus_OrderId",
                table: "BookOrderStatus");

            migrationBuilder.DropColumn(
                name: "Ingredient",
                table: "PersonalRecipe");

            migrationBuilder.DropColumn(
                name: "District_code",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Province_code",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ShopAddress",
                table: "Customer");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "RecipeTag",
                newName: "TagID");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "RecipeTag",
                newName: "RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeTag_TagId",
                table: "RecipeTag",
                newName: "IX_RecipeTag_TagID");

            migrationBuilder.RenameColumn(
                name: "NumberOfService",
                table: "Recipe",
                newName: "NumberofService");

            migrationBuilder.RenameColumn(
                name: "NumberOfService",
                table: "PersonalRecipe",
                newName: "NumberofService");

            migrationBuilder.RenameColumn(
                name: "Ward_code",
                table: "Customer",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "BookOrderStatus",
                newName: "BookOrderId");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Book",
                newName: "width");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Book",
                newName: "weight");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Book",
                newName: "length");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Book",
                newName: "height");

            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Employee",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "BookOrderStatusID",
                table: "BookOrderStatus",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<bool>(
                name: "ISBN",
                table: "Book",
                type: "bit",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

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
                name: "Ward_code",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookOrderStatus_BookOrder_BookOrderStatusID",
                table: "BookOrderStatus",
                column: "BookOrderStatusID",
                principalTable: "BookOrder",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__RecipeTag__Recip__66603565",
                table: "RecipeTag",
                column: "RecipeID",
                principalTable: "Recipe",
                principalColumn: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK__RecipeTag__TagID__6754599E",
                table: "RecipeTag",
                column: "TagID",
                principalTable: "Tag",
                principalColumn: "TagID");
        }
    }
}
