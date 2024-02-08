using Search.Common.Documents;
using Search.Common.Enums;

namespace Search.Services.Services;

public interface IIngestionService
{
    Task IngestDocuments(List<Document> documents, EntityType type, ActionType actionType,
        CancellationToken token = default);

    Task<bool> IndexIsEmpty(EntityType type, CancellationToken token = default);
}