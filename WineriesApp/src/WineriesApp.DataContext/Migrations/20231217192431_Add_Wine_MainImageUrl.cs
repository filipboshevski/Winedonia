using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineriesApp.DataContext.Migrations
{
    public partial class Add_Wine_MainImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Wine",
                newName: "PreviewImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "Wine",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Wine");

            migrationBuilder.RenameColumn(
                name: "PreviewImageUrl",
                table: "Wine",
                newName: "ImageUrl");
        }
    }
}
