using System.Globalization;
using Search.Api.Protos;
using ActionType = WineriesApp.Services.Enums.ActionType;
using Document = WineriesApp.Services.Models.Document;
using EntityType = WineriesApp.Services.Enums.EntityType;

namespace WineriesApp.Services.Clients;

public class IngestionClient
    : IIngestionClient
{
    private readonly IngestionV1.IngestionV1Client client;

    public IngestionClient(IngestionV1.IngestionV1Client client)
    {
        this.client = client;
    }

    public async Task<List<Document>> FuzzySearch(EntityType type, List<double>? ratings = null,
        List<string>? locations = null, string? searchTerm = null, int? batchIndex = null, int? batchSize = null,
        CancellationToken token = default)
    {
        var documents = new List<Document>();
        var request = new FuzzySearchRequest
        {
            EntityType = (Search.Api.Protos.EntityType)type,
            SearchTerm = searchTerm,
            Ratings = { (ratings ?? new List<double>()).Select(r => r.ToString(CultureInfo.InvariantCulture)) },
            Locations = { locations ?? new List<string>() },
            PageIndex = batchIndex,
            PageSize = batchSize
        };

        var response = await client.FuzzySearchAsync(request, cancellationToken: token);

        if (response is not null && response.Documents.Count > 0)
        {
            documents = response.Documents.Select(d => new Document
            {
                Id = Guid.Parse(d.Id),
                Location = d.Location,
                Name = d.Name,
                Rating = d.Rating
            }).ToList();
        }

        return documents;
    }

    public async Task IngestDocuments(List<Document> documents, ActionType actionType, EntityType type, CancellationToken token = default)
    {
        var request = new IngestDocumentsRequest
        {
            Documents = { documents.Select(d => new Search.Api.Protos.Document
            {
                Id = d.Id.ToString(),
                Name = d.Name,
                Location = d.Location,
                Rating = d.Rating
            }) },
            ActionType = (Search.Api.Protos.ActionType)actionType,
            EntityType = (Search.Api.Protos.EntityType)type
        };

        await client.IngestDocumentsAsync(request, cancellationToken: token);
    }

    public async Task<bool> IndexIsEmpty(EntityType type, CancellationToken token = default)
    {
        var request = new IndexIsEmptyRequest
        {
            EntityType = (Search.Api.Protos.EntityType)type
        };

        var response = await client.IndexIsEmptyAsync(request, cancellationToken: token);

        return response is not null && response.IsEmpty;
    }
}