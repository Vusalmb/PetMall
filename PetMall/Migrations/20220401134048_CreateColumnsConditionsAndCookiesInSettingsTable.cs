using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMall.Migrations
{
    public partial class CreateColumnsConditionsAndCookiesInSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cookies",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Cookies",
                table: "Settings");
        }
    }
}
