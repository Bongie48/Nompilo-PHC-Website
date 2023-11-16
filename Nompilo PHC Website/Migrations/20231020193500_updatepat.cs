using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class updatepat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Urine",
                schema: "Identity",
                table: "PrenatalPatients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                schema: "Identity",
                table: "PrenatalPatients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Urine",
                schema: "Identity",
                table: "PrenatalPatients");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "Identity",
                table: "PrenatalPatients");

        }
    }
}
