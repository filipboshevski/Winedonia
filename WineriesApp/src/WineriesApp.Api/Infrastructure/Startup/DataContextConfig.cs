using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Logging;
using System.Reflection;
using WineriesApp.DataContext;

namespace WineriesApp.Api.Infrastructure.Startup
{
    public static class DataContextConfig
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IHostEnvironment environment, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("WineriesDbConnection") ?? throw new ArgumentNullException("Connection string should not be null");

            services
                .AddDbContext<WineriesDbContext>((provider, options) =>
                {
                    options
                        .EnableDetailedErrors(environment.IsDevelopment())
                        .EnableSensitiveDataLogging(!environment.IsProduction())
                        .UseSqlServer(connectionString, server =>
                        {
                            var assembly = Assembly.GetAssembly(typeof(WineriesDbContext));

                            if (assembly != null)
                            {
                                server.MigrationsAssembly(assembly.FullName);
                            }

                            server.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        })
                        .UseLoggerFactory(provider.GetRequiredService<ILoggerFactory>());
                }, ServiceLifetime.Transient, ServiceLifetime.Singleton);

            return services;
        }

        public static async Task ApplyMigrations(this IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetService<WineriesDbContext>() ?? throw new ArgumentNullException("DbContext should not be null");
            var logger = serviceProvider.GetService<ILogger<WineriesDbContext>>() ?? throw new ArgumentNullException("Logger should not be null");
            var pendingMigrations = GetPendingMigrations(dbContext);

            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
                logger.LogInformation("Migrated to latest version {Version}", dbContext.GetLatestVersion());
            }
        }

        private static IEnumerable<string> GetPendingMigrations(this DbContext dbContext)
        {
            try
            {
                return dbContext.Database.GetPendingMigrations();
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        private static string GetLatestVersion(this DbContext dbContext)
            => dbContext.Database.GetAppliedMigrations().LastOrDefault() ?? string.Empty;
    }
}
