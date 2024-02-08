using Microsoft.EntityFrameworkCore.Migrations;
using WineriesApp.DataContext.Helpers;

#nullable disable

namespace WineriesApp.DataContext.Migrations
{
    public partial class Populate_With_Initial_Municipality_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using var dbContext = DbContextHelper.GetDbContext();
            var municipalities = MapDataHelper.GetMunicipalities();
            
            dbContext.AddRange(municipalities);
            dbContext.SaveChanges();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
