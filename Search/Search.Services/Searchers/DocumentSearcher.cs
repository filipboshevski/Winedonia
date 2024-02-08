using Nest;
using Search.Common.Documents;
using Search.Common.Enums;

namespace Search.Services.Searchers;

using Alias = Common.Aliases.Search;

public class DocumentSearcher
    : IDocumentSearcher
{
    private readonly IElasticClient client;
    
    private static readonly Dictionary<string, double> queryBoostPairs = new()
    {
        { "ExactSearch_Name", Math.Pow(2, 4) },
        { "PrefixQuery_Name", Math.Pow(2, 3.7) },
        { "PrefixQuery_Rating", Math.Pow(2, 3.3) },
        { "MatchPhrase_Name", Math.Pow(2, 3) },
        { "WildcardQuery_Name", Math.Pow(2, 2.8) },
        { "ExactSearch_Location", Math.Pow(2, 2.6) }
    };

    public DocumentSearcher(IElasticClient client)
    {
        this.client = client;
    }
    
    public async Task<List<Document>> SearchDocuments(EntityType type, string? searchTerm, List<string> ratings, List<string> locations, int? pageIndex = null, int? pageSize = null, CancellationToken token = default)
    {
        var alias = type == EntityType.Winery ? Alias.Winery : Alias.Wine;
        var request = new SearchRequest<Document>(alias)
        {
            Size = pageSize ?? 100,
            Source = new SourceFilter(),
            Sort = new List<ISort>
            {
                new FieldSort
                {
                    Field = $"{nameof(Document.Rating).ToLowerInvariant()}.keyword",
                    Order = SortOrder.Descending
                }
            }
        };

        var shouldMatch = searchTerm?.Length > 0 ? 1 : 0;
        
        var should = searchTerm?.Length > 0
                ? new List<QueryContainer> {
                    /*
                     * Exact search
                     * The search text is equvalent to the name of the document from start to end. Case-insensitive
                     */
                    new TermQuery { Field = $"{nameof(Document.Name).ToLowerInvariant()}.keyword", Value = searchTerm, CaseInsensitive = true, Boost = GetBoostValue("ExactSearch_Name"), Name = "ExactSearch_Name" },

                    /*
                     * Starts with
                     * Checks whether the name of the document starts with the search text. Case-insensitive
                     */
                    new PrefixQuery { Field = $"{nameof(Document.Name).ToLowerInvariant()}.keyword", Value = searchTerm, CaseInsensitive = true, Boost = GetBoostValue("PrefixQuery_Name"), Name = "PrefixQuery_Name" },

                    /*
                     * Phrase match
                     * Checks if all the words from the search text are present, in the specified order. Case-insensitive.
                     */
                    new MatchPhraseQuery { Field = $"{nameof(Document.Name).ToLowerInvariant()}.keyword", Query = searchTerm, Boost = GetBoostValue("MatchPhrase_Name"), Name = "MatchPhrase_Name" },
                    
                    /*
                     * Wildcard match
                     * Checks if all the words from the search text are present, in the specified order. Case-insensitive.
                     */
                    new WildcardQuery { Field = $"{nameof(Document.Name).ToLowerInvariant()}.keyword", Wildcard = $"*{GetWildcard(searchTerm)}*", CaseInsensitive = true, Boost = GetBoostValue("WildcardQuery_Name"), Name = "WildcardQuery_Name" },
                }
                : new List<QueryContainer>();

        if (locations.Count > 0)
        {
            var locationsQueries = locations.Select(location => new TermQuery
                {
                    Field = $"{nameof(Document.Location).ToLowerInvariant()}.keyword",
                    Value = location,
                    CaseInsensitive = true,
                    Boost = GetBoostValue("ExactSearch_Location"),
                    Name = "ExactSearch_Location"
                })
                .Select(q => (QueryContainer)q)
                .ToList();

            should = should.Concat(locationsQueries).ToList();
            shouldMatch++;
        }
        
        if (ratings.Count > 0)
        {
            var ratingsQueries = ratings.Select(rating => new PrefixQuery()
                {
                    Field = $"{nameof(Document.Rating).ToLowerInvariant()}.keyword",
                    Value = rating,
                    CaseInsensitive = true,
                    Boost = GetBoostValue("PrefixQuery_Rating"),
                    Name = "PrefixQuery_Rating"
                })
                .Select(q => (QueryContainer)q)
                .ToList();

            should = should.Concat(ratingsQueries).ToList();
            shouldMatch++;
        }
        
        request.Query = new BoolQuery
        {
            Should = should,
            MinimumShouldMatch = shouldMatch
        };

        if (pageIndex is not null && pageSize is not null)
        {
            request.From = pageIndex * pageSize;
            request.Size = pageSize;
        }
        
        var response = await client.SearchAsync<Document>(request, token);

        return BuildDocuments(response);
    }
    
    private static string GetWildcard(string value) => string.Join('*', value.Split(' ', StringSplitOptions.RemoveEmptyEntries));

    private List<Document> BuildDocuments(ISearchResponse<Document> response)
    {
        var hits = new List<Document>();
        foreach (var x in response.Hits)
        {
            hits.Add(new Document
            {
                Id = x.Source.Id,
                Location = x.Source.Location,
                Name = x.Source.Name,
                Rating = x.Source.Rating
            });
        }

        return hits;
    }
    
    private double GetBoostValue(string query)
    {
        queryBoostPairs.TryGetValue(query, out double boostValue);
        return boostValue;
    }
}