using Search.ElasticMigrator;
using Search.ElasticMigrator.Models;

namespace Search.Api.Infrastructure.Startup;

using Alias = Common.Aliases.Search;

public static class ElasticMigrationConfig
{
    public static async Task ApplyElasticMigrations(this IServiceProvider services,
        IConfiguration configuration)
    {
        var migrator = services.GetService<IElasticMigrator>();

        if (migrator is not null)
        {
            var clusterNode = configuration.GetValue<string>("ElasticSearch:NodeName") ?? throw new ArgumentNullException("Cluster node name must not be null.");

            var migrations = configuration.GetSection("ElasticSearch:CurrentMigrations").GetChildren();

            foreach (var migration in migrations)
            {
                var indexName = migration.GetChildren().FirstOrDefault(c => c.Key == "Name")?.Value ??
                           throw new Exception("Migration name should not be null");
                var version = migration.GetChildren().FirstOrDefault(c => c.Key == "Version")?.Value ??
                              throw new Exception("Migration version should not be null");
                var alias = migration.GetChildren().FirstOrDefault(c => c.Key == "Alias")?.Value ??
                              throw new Exception("Migration alias should not be null");
                
                await migrator.MigrateAsync(new IndexMigration { NodeName = clusterNode, IndexVersion = version, IndexName = indexName, Aliases = new () { alias }});
            }
        }
    }
}