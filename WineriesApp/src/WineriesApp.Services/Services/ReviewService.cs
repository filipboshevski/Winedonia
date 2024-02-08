using Microsoft.EntityFrameworkCore;
using WineriesApp.DataContext;
using WineriesApp.DataContext.Enums;
using WineriesApp.DataContext.Models;
using WineriesApp.Services.Clients;
using WineriesApp.Services.Enums;
using WineriesApp.Services.Models;

namespace WineriesApp.Services.Services;

public class ReviewService : IReviewService
{
    private readonly WineriesDbContext context;
    private readonly IIngestionClient ingestionClient;

    public ReviewService(WineriesDbContext context, IIngestionClient ingestionClient)
    {
        this.context = context;
        this.ingestionClient = ingestionClient;
    }
    
    public Task<List<Review>> GetWineryReviews(Guid wineryId)
    {
        return context.Reviews.Where(r => r.Type == ReviewEntityType.Winery && r.Winery!.Id == wineryId).OrderByDescending(r => r.Rating).ToListAsync();
    }

    public Task<List<Review>> GetWineReviews(Guid wineId)
    {
        return context.Reviews.Where(r => r.Type == ReviewEntityType.Wine && r.Wine!.Id == wineId).OrderByDescending(r => r.Rating).ToListAsync();
    }

    public async Task AddReview(AddReview model)
    {
        var review = new Review
        {
            Rating = model.Rating,
            Comment = model.Comment,
            Type = model.EntityType,
            Date = DateTime.Now
        };

        switch (model.EntityType)
        {
            case ReviewEntityType.Winery:
                var winery = await context.Wineries.Include(winery => winery.Municipality).FirstOrDefaultAsync(w => w.Id == model.EntityId);
                winery!.Rating = Math.Min(Math.Round(0.95 * winery.Rating + 0.05 * model.Rating, 2), 5.0);
                review.Winery = winery;
                await IngestEntityChange(EntityType.Winery, new Document
                {
                    Id = winery.Id,
                    Name = winery.Name,
                    Rating = winery.Rating,
                    Location = winery.Municipality?.Name
                });
                break;
            case ReviewEntityType.Wine:
                var wine = await context.Wines.FirstOrDefaultAsync(w => w.Id == model.EntityId);
                wine!.Rating = Math.Min(Math.Round(0.95 * wine.Rating + 0.05 * model.Rating, 2), 5.0);
                review.Wine = wine;
                await IngestEntityChange(EntityType.Wine, new Document
                {
                    Id = wine.Id,
                    Name = wine.Name,
                    Rating = wine.Rating
                });
                break;
            default:
                throw new Exception("Unknown entity type");
        }

        await context.Reviews.AddAsync(review);
        await context.SaveChangesAsync();
    }

    private async Task IngestEntityChange(EntityType type, Document document)
    {
        await ingestionClient.IngestDocuments(new List<Document> { document }, ActionType.Update, type);
    }
}