using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class ModifyCheckUps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodPressure",
                schema: "Identity",
                table: "Checkups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrenatalMonth",
                schema: "Identity",
                table: "Checkups",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Temperature",
                schema: "Identity",
                table: "Checkups",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "Identity",
                table: "Checkups",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodPressure",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropColumn(
                name: "PrenatalMonth",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropColumn(
                name: "Temperature",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "Identity",
                table: "Checkups");
        }
    }
}
