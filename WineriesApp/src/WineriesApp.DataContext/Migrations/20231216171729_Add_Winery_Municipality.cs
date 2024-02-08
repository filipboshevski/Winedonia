using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineriesApp.DataContext.Migrations
{
    public partial class Add_Winery_Municipality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winery_Municipality_LocationId",
                table: "Winery");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "Winery");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Winery",
                newName: "MunicipalityId");

            migrationBuilder.RenameIndex(
                name: "IX_Winery_LocationId",
                table: "Winery",
                newName: "IX_Winery_MunicipalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Winery_Municipality_MunicipalityId",
                table: "Winery",
                column: "MunicipalityId",
                principalTable: "Municipality",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Winery_Municipality_MunicipalityId",
                table: "Winery");

            migrationBuilder.RenameColumn(
                name: "MunicipalityId",
                table: "Winery",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Winery_MunicipalityId",
                table: "Winery",
                newName: "IX_Winery_LocationId");

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "Winery",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Winery_Municipality_LocationId",
                table: "Winery",
                column: "LocationId",
                principalTable: "Municipality",
                principalColumn: "Id");
        }
    }
}
