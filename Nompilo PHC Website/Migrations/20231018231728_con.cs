using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class con : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                schema: "Identity",
                table: "ContraceptiveRecordN",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "QSym",
                schema: "Identity",
                table: "ContraceptiveRecordN",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Symptoms",
                schema: "Identity",
                table: "ContraceptiveRecordN",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QSym",
                schema: "Identity",
                table: "ContraceptiveRecordN");

            migrationBuilder.DropColumn(
                name: "Symptoms",
                schema: "Identity",
                table: "ContraceptiveRecordN");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityNumber",
                schema: "Identity",
                table: "ContraceptiveRecordN",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);
        }
    }
}
