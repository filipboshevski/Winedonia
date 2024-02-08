using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Search.Api.Protos;
using Search.Services.Searchers;
using Search.Services.Services;

namespace Search.Api.Services;

public class IngestionServiceV1
    : IngestionV1.IngestionV1Base
{
    private readonly IDocumentSearcher searcher;
    private readonly IIngestionService ingestionService;

    public IngestionServiceV1(IDocumentSearcher searcher, IIngestionService ingestionService)
    {
        this.searcher = searcher;
        this.ingestionService = ingestionService;
    }
    
    public override async Task<FuzzySearchResponse> FuzzySearch(FuzzySearchRequest request, ServerCallContext context)
    {
        var documents = await searcher.SearchDocuments((Common.Enums.EntityType)request.EntityType, request.SearchTerm,
            request.Ratings.ToList(), request.Locations.ToList(), request.PageIndex, request.PageSize);
        
        var response = new FuzzySearchResponse();
        
        response.Documents.AddRange(documents.Select(x => new Document
        {
            Id = x.Id.ToString(),
            Location = x.Location,
            Name = x.Name,
            Rating = x.Rating
        }));
        response.Count = documents.Count;
        
        return response;
    }

    public override async Task<Empty> IngestDocuments(IngestDocumentsRequest request, ServerCallContext context)
    {
        var documents = request.Documents.Select(d => new Common.Documents.Document
        {
            Id = Guid.Parse(d.Id),
            Name = d.Name,
            Location = d.Location,
            Rating = d.Rating
        }).ToList();

        await ingestionService.IngestDocuments(documents, (Common.Enums.EntityType)request.EntityType, (Common.Enums.ActionType)request.ActionType);

        return new Empty();
    }

    public override async Task<IndexIsEmptyResponse> IndexIsEmpty(IndexIsEmptyRequest request, ServerCallContext context)
    {
        var response = new IndexIsEmptyResponse
        {
            IsEmpty = await ingestionService.IndexIsEmpty((Common.Enums.EntityType)request.EntityType)
        };
        
        return response;
    }
}