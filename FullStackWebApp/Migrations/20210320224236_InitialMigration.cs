using Microsoft.EntityFrameworkCore.Migrations;

namespace FullStackWebApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aanbod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    GUID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tuin = table.Column<bool>(type: "bit", nullable: false),
                    MakelaarId = table.Column<int>(type: "int", nullable: false),
                    MakelaarNaam = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanbod", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aanbod");
        }
    }
}
