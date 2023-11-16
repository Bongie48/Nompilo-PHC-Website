using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class kuhiuhi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SympClass",
                schema: "Identity",
                columns: table => new
                {
                    SyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QSym = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SympClass", x => x.SyID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SympClass",
                schema: "Identity");
        }
    }
}
