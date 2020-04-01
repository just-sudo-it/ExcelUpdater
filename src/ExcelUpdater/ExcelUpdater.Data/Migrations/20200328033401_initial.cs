using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExcelUpdater.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    My_Date = table.Column<DateTime>(nullable: false),
                    My_Id = table.Column<string>(nullable: false),
                    Time1 = table.Column<float>(nullable: false),
                    Time2 = table.Column<float>(nullable: false),
                    Time3 = table.Column<float>(nullable: false),
                    Time4 = table.Column<float>(nullable: false),
                    Time5 = table.Column<float>(nullable: false),
                    Time6 = table.Column<float>(nullable: false),
                    Time7 = table.Column<float>(nullable: false),
                    Time8 = table.Column<float>(nullable: false),
                    Time9 = table.Column<float>(nullable: false),
                    Time10 = table.Column<float>(nullable: false),
                    Time11 = table.Column<float>(nullable: false),
                    Time12 = table.Column<float>(nullable: false),
                    Time13 = table.Column<float>(nullable: false),
                    Time14 = table.Column<float>(nullable: false),
                    Time15 = table.Column<float>(nullable: false),
                    Time16 = table.Column<float>(nullable: false),
                    Time17 = table.Column<float>(nullable: false),
                    Time18 = table.Column<float>(nullable: false),
                    Time19 = table.Column<float>(nullable: false),
                    Time20 = table.Column<float>(nullable: false),
                    Time21 = table.Column<float>(nullable: false),
                    Time22 = table.Column<float>(nullable: false),
                    Time23 = table.Column<float>(nullable: false),
                    Time24 = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => new { x.My_Id, x.My_Date });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}
