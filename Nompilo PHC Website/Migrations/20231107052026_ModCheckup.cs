using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class ModCheckup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                schema: "Identity",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                schema: "Identity",
                table: "Checkups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PatientId",
                schema: "Identity",
                table: "Checkups",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DoctorId",
                schema: "Identity",
                table: "Prescriptions",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkups_DoctorId",
                schema: "Identity",
                table: "Checkups",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkups_PatientId",
                schema: "Identity",
                table: "Checkups",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkups_AspNetUsers_DoctorId",
                schema: "Identity",
                table: "Checkups",
                column: "DoctorId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkups_AspNetUsers_PatientId",
                schema: "Identity",
                table: "Checkups",
                column: "PatientId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_AspNetUsers_DoctorId",
                schema: "Identity",
                table: "Prescriptions",
                column: "DoctorId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkups_AspNetUsers_DoctorId",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkups_AspNetUsers_PatientId",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_AspNetUsers_DoctorId",
                schema: "Identity",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DoctorId",
                schema: "Identity",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Checkups_DoctorId",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropIndex(
                name: "IX_Checkups_PatientId",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "Identity",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "Identity",
                table: "Checkups");

            migrationBuilder.DropColumn(
                name: "PatientId",
                schema: "Identity",
                table: "Checkups");
        }
    }
}
