using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class kjhf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nausea",
                schema: "Identity",
                table: "LogSyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Spotting",
                schema: "Identity",
                table: "LogSyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Weight",
                schema: "Identity",
                table: "LogSyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "period",
                schema: "Identity",
                table: "LogSyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "vaginal",
                schema: "Identity",
                table: "LogSyms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "Identity",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nausea",
                schema: "Identity",
                table: "LogSyms");

            migrationBuilder.DropColumn(
                name: "Spotting",
                schema: "Identity",
                table: "LogSyms");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "Identity",
                table: "LogSyms");

            migrationBuilder.DropColumn(
                name: "period",
                schema: "Identity",
                table: "LogSyms");

            migrationBuilder.DropColumn(
                name: "vaginal",
                schema: "Identity",
                table: "LogSyms");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "Identity",
                table: "Instruments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
