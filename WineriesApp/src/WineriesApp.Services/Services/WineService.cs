using Microsoft.EntityFrameworkCore;
using WineriesApp.DataContext;
using WineriesApp.DataContext.Models;
using WineriesApp.Services.Clients;
using WineriesApp.Services.Enums;
using WineriesApp.Services.Models;
using WineriesApp.Services.Models.Filters;

namespace WineriesApp.Services.Services;

public class WineService : IWineService
{
    private readonly WineriesDbContext context;
    private readonly IIngestionClient client;

    public WineService(WineriesDbContext context, IIngestionClient client)
    {
        this.context = context;
        this.client = client;
    }
    
    public async Task<List<Wine>> FilterWines(WinesFilter filter, CancellationToken token = default)
    {
        var wines = new List<Wine>();
        var searchHits = await client.FuzzySearch(EntityType.Wine, ratings: filter.Ratings.ToList(),
            searchTerm: filter.SearchTerm, batchSize: filter.BatchIndex, batchIndex: filter.BatchSize, token: token);

        if (!searchHits.Any())
        {
            return wines;
        }

        var hitsIds = searchHits.Select(h => h.Id).ToList();

        var hits = context.Wines.Where(w => hitsIds.Contains(w.Id));

        if (filter.Types.Length > 0)
        {
            hits = hits.Where(w => filter.Types.Contains(w.Type));
        }

        return await hits.ToListAsync(token);
    }

    public Task<Wine?> GetWine(Guid id)
    {
        return context.Wines.Include(w => w.Wineries).FirstOrDefaultAsync(w => w.Id == id);
    }
    
    public async Task IngestWines(CancellationToken token = default)
    {
        if (!await client.IndexIsEmpty(EntityType.Wine, token))
        {
            return;
        }
            
        var wines = await context.Wines.ToListAsync(token);
        var documents = wines.Select(w => new Document
        {
            Id = w.Id,
            Name = w.Name,
            Rating = w.Rating
        }).ToList();

        await client.IngestDocuments(documents, ActionType.Ingest, EntityType.Wine, token);
    }
}