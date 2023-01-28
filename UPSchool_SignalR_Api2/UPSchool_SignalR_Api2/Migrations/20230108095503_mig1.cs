using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UPSchool_SignalR_Api2.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elektriks",
                columns: table => new
                {
                    ElektrikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    ElectricDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elektriks", x => x.ElektrikId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Elektriks");
        }
    }
}
