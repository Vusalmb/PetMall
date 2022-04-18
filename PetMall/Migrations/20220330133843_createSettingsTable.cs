using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMall.Migrations
{
    public partial class createSettingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(nullable: true),
                    LogoImage = table.Column<string>(nullable: true),
                    LogoDesc = table.Column<string>(nullable: true),
                    SearchIcon = table.Column<string>(nullable: true),
                    BasketIcon = table.Column<string>(nullable: true),
                    SettingIcon = table.Column<string>(nullable: true),
                    ScrollTopIcon = table.Column<string>(nullable: true),
                    FacebookIcon = table.Column<string>(nullable: true),
                    PinterestIcon = table.Column<string>(nullable: true),
                    InstagramIcon = table.Column<string>(nullable: true),
                    TweeterIcon = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
