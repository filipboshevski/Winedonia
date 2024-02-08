using Nest;
using Search.Common.Documents;
using Search.Common.Interfaces;
using Search.ElasticMigrator.Models;
using Serilog;
using Index = Nest.Index;

namespace Search.ElasticMigrator;

public class ElasticMigrator
    : IElasticMigrator
{
    private readonly IElasticClient client;
    private readonly ILogger logger;
    private readonly IIndexer indexer;

    public ElasticMigrator(IElasticClient client, ILogger logger, IIndexer indexer)
    {
        this.client = client;
        this.logger = logger;
        this.indexer = indexer;
    }
    
    public async Task MigrateAsync(IndexMigration migration)
    {
        var indexName = $"{migration.IndexName}-{migration.IndexVersion}";
        
        if (await IndexExistsAsync(indexName))
        {
            return;
        }
        
        await client.Indices.CreateAsync(indexName, c => c
            .Settings(s => s
                    .Setting("index.routing.allocation.include.name", migration.NodeName)
                    .NumberOfShards(2)
                    .NumberOfReplicas(0)
            )
            .Map<Document>(m => m
                .AutoMap()
                .Properties(p => p
                    .Text(t => t
                        .Name(n => n.Name)
                        .Fields(f => f
                            .Keyword(k => k
                                    .Name("keyword")
                            )
                        )
                    )
                    .Number(n => n
                            .Name(n => n.Rating)
                            .Fields(f => f
                                .Keyword(k => k
                                    .Name("keyword")
                                )
                            )
                    )
                    .Text(t => t
                            .Name(n => n.Location)
                            .Fields(f => f
                                .Keyword(k => k
                                    .Name("keyword")
                                )
                            )
                    )
                )
            )
        );

        foreach (var alias in migration.Aliases)
        {
            var putAliasResponse = await client.Indices.PutAliasAsync(indexName, alias);
        
            if (putAliasResponse.IsValid)
            {
                logger.Information($"Created alias {alias} for index {indexName}");
            }
            else
            {
                logger.Error($"Failed to create alias {alias} for index {indexName}", putAliasResponse.ServerError.Error);
            }
        }
    }

    public async Task<bool> IndexExistsAsync(string indexName)
    {
        var indexExistsResponse = await client.Indices.ExistsAsync(indexName);
        return indexExistsResponse.Exists;
    }
}