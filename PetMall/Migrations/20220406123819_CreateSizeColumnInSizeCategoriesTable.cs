using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMall.Migrations
{
    public partial class CreateSizeColumnInSizeCategoriesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sizes_SizeCategoryId",
                table: "Sizes");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_SizeCategoryId",
                table: "Sizes",
                column: "SizeCategoryId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sizes_SizeCategoryId",
                table: "Sizes");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_SizeCategoryId",
                table: "Sizes",
                column: "SizeCategoryId");
        }
    }
}
