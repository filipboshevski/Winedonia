using Nest;
using Search.Common.Interfaces;
using Serilog;

namespace Search.Common.Indexers;

public class Indexer
    : IIndexer
{
    private readonly IElasticClient client;
    private readonly ILogger logger;

    public Indexer(IElasticClient client, ILogger logger)
    {
        this.client = client;
        this.logger = logger;
    }
    
    public async Task IndexSingle<T>(string indexName, T document, CancellationToken token = default) 
        where T : class, IDocument
    {
        var response = await client.IndexAsync(document, r => r.Index(indexName), token);

        if (!response.IsValid || response.ServerError is not null)
        {
            throw new Exception("Unable to index document.", response.OriginalException);
        }
        
        logger.Information("Indexed document {@Document} into index {IndexName}.", document, indexName);
    }

    public Task IndexMultiple<T>(string indexName, IEnumerable<T> documents, int timeoutInMinutes = 10, CancellationToken token = default)
        where T : class, IDocument
    {
        if (!documents.Any())
        {
            return Task.CompletedTask;
        }
        
        var operation = client.BulkAll(
            documents,
            request => request
                .Index(Indices.Index(indexName))
                .MaxDegreeOfParallelism(Environment.ProcessorCount)
                .Size(100)
                .BackOffTime("30s")
                .BackOffRetries(3)
                .RefreshOnCompleted()
                .DroppedDocumentCallback((_, document) =>
                {
                    logger.Error("Unable to index document {@Document} into index {IndexName}", document, indexName);
                }),
            cancellationToken: token);
        
        operation.Wait(TimeSpan.FromMinutes(timeoutInMinutes), next =>
        {
            logger.Information("Indexed {DocumentsCount} documents into index {IndexName}.", next.Items.Count, indexName);
        });

        return Task.CompletedTask;
    }
}