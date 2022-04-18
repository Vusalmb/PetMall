using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMall.Migrations
{
    public partial class CreateShopSizesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopSizeId",
                table: "Shops",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShopSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopSizes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ShopSizeId",
                table: "Shops",
                column: "ShopSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shops_ShopSizes_ShopSizeId",
                table: "Shops",
                column: "ShopSizeId",
                principalTable: "ShopSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shops_ShopSizes_ShopSizeId",
                table: "Shops");

            migrationBuilder.DropTable(
                name: "ShopSizes");

            migrationBuilder.DropIndex(
                name: "IX_Shops_ShopSizeId",
                table: "Shops");

            migrationBuilder.DropColumn(
                name: "ShopSizeId",
                table: "Shops");
        }
    }
}
