using Search.Common.Documents;
using Search.ElasticMigrator.Models;

namespace Search.ElasticMigrator;

public interface IElasticMigrator
{
    Task MigrateAsync(IndexMigration migration);

    Task<bool> IndexExistsAsync(string indexName);
}