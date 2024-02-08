using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineriesApp.DataContext.Migrations
{
    public partial class Add_Review : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WineryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Wine_WineId",
                        column: x => x.WineId,
                        principalTable: "Wine",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_Winery_WineryId",
                        column: x => x.WineryId,
                        principalTable: "Winery",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_WineId",
                table: "Review",
                column: "WineId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_WineryId",
                table: "Review",
                column: "WineryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
