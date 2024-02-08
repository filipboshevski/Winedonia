using Microsoft.EntityFrameworkCore;
using WineriesApp.DataContext;
using WineriesApp.DataContext.Models;
using WineriesApp.Services.Clients;
using WineriesApp.Services.Enums;
using WineriesApp.Services.Models;
using WineriesApp.Services.Models.Filters;

namespace WineriesApp.Services.Services
{
    public class WineryService : IWineryService
    {
        private readonly WineriesDbContext context;
        private readonly IIngestionClient client;

        public WineryService(WineriesDbContext context, IIngestionClient client)
        {
            this.context = context;
            this.client = client;
        }

        public Task<List<Winery>> GetTopWineries()
        {
            return context.Wineries.OrderByDescending(w => w.Rating).Take(12).ToListAsync();
        }

        public async Task<List<Winery>> FilterWineries(WineriesFilter filter, CancellationToken token = default)
        {
            var wineries = new List<Winery>();
            var locations = new List<string>();

            if (filter.Locations.Length > 0)
            {
                locations = await context.Municipalities.Where(m => filter.Locations.Contains(m.Id)).Select(m => m.Name)
                    .Distinct().ToListAsync(token);
            }
            
            var searchHits = await client.FuzzySearch(EntityType.Winery, filter.Ratings.ToList(),
                locations, filter.SearchTerm, filter.BatchIndex, filter.BatchSize + 1, token);

            if (!searchHits.Any())
            {
                return wineries;
            }

            var hitsIds = searchHits.Select(h => h.Id).ToList();

            return await context.Wineries.Where(w => hitsIds.Contains(w.Id)).OrderByDescending(w => w.Rating).ToListAsync(token);
        }

        public async Task<Winery?> GetWinery(Guid id)
        {
            return context.Wineries.FirstOrDefault(w => w.Id == id);
        }

        public async Task IngestWineries(CancellationToken token = default)
        {
            if (!await client.IndexIsEmpty(EntityType.Winery, token))
            {
                return;
            }
            
            var wineries = await context.Wineries.Include(w => w.Municipality).ToListAsync(token);
            var documents = wineries.Select(w => new Document
            {
                Id = w.Id,
                Location = w.Municipality?.Name,
                Name = w.Name,
                Rating = w.Rating
            }).ToList();

            await client.IngestDocuments(documents, ActionType.Ingest, EntityType.Winery, token);
        }
    }
}
