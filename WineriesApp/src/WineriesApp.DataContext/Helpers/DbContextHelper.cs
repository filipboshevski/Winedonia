using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WineriesApp.DataContext.Helpers
{
    public static class DbContextHelper
    {
        public static WineriesDbContext GetDbContext()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();
            
            var connectionString = configuration.GetConnectionString("WineriesDbConnection") ?? throw new ArgumentNullException("Connection string should not be null");
            var optionsBuilder = new DbContextOptionsBuilder<WineriesDbContext>();
            
            optionsBuilder.UseSqlServer(connectionString);
            
            return new WineriesDbContext(optionsBuilder.Options);
        }
    }
}
