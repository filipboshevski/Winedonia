using Microsoft.EntityFrameworkCore.Migrations;
using WineriesApp.DataContext.Helpers;

#nullable disable

namespace WineriesApp.DataContext.Migrations
{
    public partial class Populate_With_Initial_Wine_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using var dbContext = DbContextHelper.GetDbContext();
            var wines = MapDataHelper.GetWines();
            var relationships = MapDataHelper.GetWinesWineries();

            if (!wines.Any())
            {
                return;
            }

            foreach (var wine in wines)
            {
                foreach (var winery in relationships[wine.Name])
                {
                    var existingWinery = dbContext.Wineries.FirstOrDefault(w => w.Name == winery);
                    
                    if (existingWinery != null)
                    {
                        existingWinery.Wines.Add(wine);
                        wine.Wineries.Add(existingWinery);
                    }
                }
            }

            dbContext.Wines.AddRange(wines);
            dbContext.SaveChanges();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
