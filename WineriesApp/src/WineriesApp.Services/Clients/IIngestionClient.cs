using Document = WineriesApp.Services.Models.Document;
using EntityType = WineriesApp.Services.Enums.EntityType;
using ActionType = WineriesApp.Services.Enums.ActionType;

namespace WineriesApp.Services.Clients;

public interface IIngestionClient
{
    Task<List<Document>> FuzzySearch(EntityType type, List<double>? ratings = null,
        List<string>? locations = null, string? searchTerm = null, int? batchIndex = null, int? batchSize = null,
        CancellationToken token = default);

    Task IngestDocuments(List<Document> documents, ActionType actionType, EntityType type, CancellationToken token = default);

    Task<bool> IndexIsEmpty(EntityType type, CancellationToken token = default);
}