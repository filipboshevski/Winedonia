using Search.Common.Documents;
using Search.Common.Enums;

namespace Search.Services.Searchers;

public interface IDocumentSearcher
{
    Task<List<Document>> SearchDocuments(EntityType type, string? searchTerm, List<string> ratings,
        List<string> locations, int? pageIndex = null, int? pageSize = null, CancellationToken token = default);
}