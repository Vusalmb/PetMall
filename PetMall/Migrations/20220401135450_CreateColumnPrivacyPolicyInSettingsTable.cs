using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMall.Migrations
{
    public partial class CreateColumnPrivacyPolicyInSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrivacyPolicy",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivacyPolicy",
                table: "Settings");
        }
    }
}
