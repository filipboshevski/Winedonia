using Microsoft.EntityFrameworkCore;
using WineriesApp.DataContext.Extensions;
using WineriesApp.DataContext.Models;

namespace WineriesApp.DataContext
{
    public class WineriesDbContext : DbContext
    {
        public WineriesDbContext(DbContextOptions<WineriesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralizingTableNameConvention();

            modelBuilder.Entity<Winery>()
                .HasMany(w => w.Wines)
                .WithMany(w => w.Wineries)
                .UsingEntity(
                    "Wine_Winery",
                    l => l.HasOne(typeof(Wine)).WithMany().HasForeignKey("WineId").HasPrincipalKey(nameof(Wine.Id)),
                    r => r.HasOne(typeof(Winery)).WithMany().HasForeignKey("WineryId").HasPrincipalKey(nameof(Winery.Id)),
                    j => j.HasKey("WineryId", "WineId"));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Winery> Wineries { get; set; }
        
        public DbSet<Wine> Wines { get; set; }

        public DbSet<Review> Reviews { get; set; }
        
        public DbSet<Municipality> Municipalities { get; set; }
    }
}