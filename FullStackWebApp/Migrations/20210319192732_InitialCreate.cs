using Microsoft.EntityFrameworkCore.Migrations;

namespace FullStackWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Makelaar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makelaar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Aanbod",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tuin = table.Column<bool>(type: "bit", nullable: false),
                    MakelaarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanbod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aanbod_Makelaar_MakelaarId",
                        column: x => x.MakelaarId,
                        principalTable: "Makelaar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aanbod_MakelaarId",
                table: "Aanbod",
                column: "MakelaarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aanbod");

            migrationBuilder.DropTable(
                name: "Makelaar");
        }
    }
}
