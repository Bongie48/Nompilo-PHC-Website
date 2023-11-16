using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class BookingMods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "status",
                schema: "Identity",
                table: "TestMethods");

            migrationBuilder.DropColumn(
                name: "PatientId",
                schema: "Identity",
                table: "Bookings");

         
        }
    }
}
