using Microsoft.EntityFrameworkCore.Migrations;

namespace PetMall.Migrations
{
    public partial class CreateEmployeesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Specialty = table.Column<string>(maxLength: 50, nullable: false),
                    Desc = table.Column<string>(maxLength: 500, nullable: false),
                    Phone = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    FacebookIcon = table.Column<string>(nullable: false),
                    FacebookUrl = table.Column<string>(maxLength: 150, nullable: true),
                    TweeterIcon = table.Column<string>(nullable: false),
                    TweeterUrl = table.Column<string>(maxLength: 150, nullable: true),
                    PinterestIcon = table.Column<string>(nullable: false),
                    PinterestUrl = table.Column<string>(maxLength: 150, nullable: true),
                    InstagramIcon = table.Column<string>(nullable: false),
                    InstagramUrl = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
