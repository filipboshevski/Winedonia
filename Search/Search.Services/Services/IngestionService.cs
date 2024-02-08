using Microsoft.Extensions.Configuration;
using Nest;
using Search.Common.Documents;
using Search.Common.Enums;
using Search.Common.Interfaces;
using Serilog;
using ActionType = Search.Common.Enums.ActionType;

namespace Search.Services.Services;

using Alias = Common.Aliases.Search;

public class IngestionService
    : IIngestionService
{
    private readonly IElasticClient client;
    private readonly ILogger logger;
    private readonly IIndexer indexer;
    private readonly IConfiguration configuration;

    public IngestionService(IElasticClient client, ILogger logger, IIndexer indexer, IConfiguration configuration)
    {
        this.client = client;
        this.logger = logger;
        this.indexer = indexer;
        this.configuration = configuration;
    }
    
    public async Task IngestDocuments(List<Document> documents, EntityType type, ActionType actionType, CancellationToken token = default)
    {
        switch (actionType)
        {
            case ActionType.Ingest:
                await IngestDocumentsAsync(documents, type, token);
                break;
            case ActionType.Update:
                await UpdateDocumentsAsync(documents, type, token);
                break;
        }
    }

    public async Task<bool> IndexIsEmpty(EntityType type, CancellationToken token = default)
    {
        var indexName = GetLatestIndexName(type);
        return !await IndexHasRowsAsync(indexName);
    }

    
    private async Task<bool> IndexHasRowsAsync(string indexName)
    {
        var indexExistsResponse = await client.CountAsync<Document>(c => c.Index(indexName));;
        return indexExistsResponse.Count > 0;
    }

    private async Task UpdateDocumentsAsync(List<Document> documents, EntityType type,
        CancellationToken token = default)
    {
        var indexName = GetLatestIndexName(type);
        var alias = type == EntityType.Winery ? Alias.Winery : Alias.Wine;

        foreach (var document in documents)
        {
            var response = await client.SearchAsync<Document>(request =>
                request
                    .Index(alias)
                    .Take(1)
                    .Query(query =>
                        query
                            .Match(match =>
                                match
                                    .Field($"{nameof(Document.Id).ToLower()}")
                                    .Query(document.Id.ToString()))), token);

            if (response.ServerError is not null)
            {
                logger.Error($"An error occurred while retrieving document with id {document.Id}", response.OriginalException);
                continue;
            }

            var savedDocument = response.Documents.FirstOrDefault();

            if (savedDocument is null)
            {
                logger.Information($"A document with id {document.Id} has not been found");
                continue;
            }

            var updatedDocument = new Document
            {
                Id = document.Id,
                Location = document.Location,
                Name = document.Name,
                Rating = document.Rating
            };

            var updateResponse =
                await client.UpdateAsync<Document>(document.Id,
                    req => req.Index(indexName).Doc(updatedDocument).DocAsUpsert(), token);
            
            if (updateResponse.Result == Result.Noop)
            {
                logger.Information("UpdateDocumentsAsync, Nothing to update, Id: {id}, Status {status}, Index {index}", updateResponse.Id, updateResponse.Result.ToString(), updateResponse.Index);
            }
            else if (updateResponse.Result != Result.Updated)
            {
                logger.Error(updateResponse.OriginalException?.Message ?? $"UpdateDocumentsAsync, Response type expected {Result.Updated}, was {updateResponse.Result}");
            }
            else
            {
                logger.Information("UpdateDocumentsAsync, Updated success, Id: {id}, Status {status}, Index {index}", updateResponse.Id, updateResponse.Result.ToString(), updateResponse.Index);
            }
        }
    }

    private async Task IngestDocumentsAsync(List<Document> documents, EntityType type, CancellationToken token = default)
    {
        var indexName = GetLatestIndexName(type);
        await indexer.IndexMultiple(indexName, documents, token: token);
    }

    private string GetLatestIndexName(EntityType entityType)
    {
        var migrationInfo = configuration.GetSection($"ElasticSearch:CurrentMigrations:{entityType.ToString()}").GetChildren();
        var indexName = migrationInfo.FirstOrDefault(i => i.Key == "Name")?.Value ?? throw new Exception("Migration name should not be null");
        var indexVersion = migrationInfo.FirstOrDefault(i => i.Key == "Version")?.Value ?? throw new Exception("Migration version should not be null");

        return $"{indexName}-{indexVersion}";
    }
}