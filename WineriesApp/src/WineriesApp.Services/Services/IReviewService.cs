using WineriesApp.DataContext.Enums;
using WineriesApp.DataContext.Models;
using WineriesApp.Services.Models;

namespace WineriesApp.Services.Services;

public interface IReviewService
{
    Task<List<Review>> GetWineryReviews(Guid wineryId);

    Task<List<Review>> GetWineReviews(Guid wineId);
    
    Task AddReview(AddReview model);
}