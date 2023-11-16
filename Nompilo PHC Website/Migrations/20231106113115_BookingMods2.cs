using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class BookingMods2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_RegisterFPatients_PatientId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_PatientId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PatientId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "UseId",
                schema: "Identity",
                table: "RegisterFPatients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DataGeeksUserId",
                schema: "Identity",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DataGeeksUserId",
                schema: "Identity",
                table: "Bookings",
                column: "DataGeeksUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_AspNetUsers_DataGeeksUserId",
                schema: "Identity",
                table: "Bookings",
                column: "DataGeeksUserId",
                principalSchema: "Identity",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_AspNetUsers_DataGeeksUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DataGeeksUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "UseId",
                schema: "Identity",
                table: "RegisterFPatients");

            migrationBuilder.DropColumn(
                name: "DataGeeksUserId",
                schema: "Identity",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                schema: "Identity",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PatientId",
                schema: "Identity",
                table: "Bookings",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_RegisterFPatients_PatientId",
                schema: "Identity",
                table: "Bookings",
                column: "PatientId",
                principalSchema: "Identity",
                principalTable: "RegisterFPatients",
                principalColumn: "RegId");
        }
    }
}
