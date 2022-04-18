using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMall.Migrations
{
    public partial class CreateColumnOurCompanyAndOurTeamInSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OurCompany",
                table: "Settings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OurTeam",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OurCompany",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "OurTeam",
                table: "Settings");
        }
    }
}
