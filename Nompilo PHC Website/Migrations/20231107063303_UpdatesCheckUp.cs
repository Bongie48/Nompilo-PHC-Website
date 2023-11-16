using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class UpdatesCheckUp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                schema: "Identity",
                table: "Tests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                schema: "Identity",
                table: "Tests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_DoctorId",
                schema: "Identity",
                table: "Tests",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tests_PatientId",
                schema: "Identity",
                table: "Tests",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_DoctorId",
                schema: "Identity",
                table: "Tests",
                column: "DoctorId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_PatientId",
                schema: "Identity",
                table: "Tests",
                column: "PatientId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_DoctorId",
                schema: "Identity",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_PatientId",
                schema: "Identity",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_DoctorId",
                schema: "Identity",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_PatientId",
                schema: "Identity",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "Identity",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "PatientId",
                schema: "Identity",
                table: "Tests");
        }
    }
}
