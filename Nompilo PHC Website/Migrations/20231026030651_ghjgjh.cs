using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nompilo_PHC_Website.Migrations
{
    public partial class ghjgjh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckType",
                schema: "Identity",
                columns: table => new
                {
                    RegId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckRegister = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckType", x => x.RegId);
                });

            migrationBuilder.CreateTable(
                name: "EmeContra",
                schema: "Identity",
                columns: table => new
                {
                    EmeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    EmailAddres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QSym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NurseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SexLength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodNorm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SexOcc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EHCBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EHCLastPeriod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EHCSym = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EHCSymDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Alleg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllegDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreMed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreMedDe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LiverDis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BreastF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vomit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContracName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContracNameD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmeContra", x => x.EmeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckType",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "EmeContra",
                schema: "Identity");
        }
    }
}
