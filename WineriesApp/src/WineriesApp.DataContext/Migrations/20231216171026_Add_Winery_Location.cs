using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineriesApp.DataContext.Migrations
{
    public partial class Add_Winery_Location : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Winery",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Winery_LocationId",
                table: "Winery",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Winery_Municipality_LocationId",
                table: "Winery",
                column: "LocationId",
                principalTable: "Municipality",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winery_Municipality_LocationId",
                table: "Winery");

            migrationBuilder.DropIndex(
                name: "IX_Winery_LocationId",
                table: "Winery");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Winery");
        }
    }
}
