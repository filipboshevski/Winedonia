using Microsoft.AspNetCore.Mvc;
using WineriesApp.DataContext.Enums;
using WineriesApp.DataContext.Models;
using WineriesApp.Services.Mappers;
using WineriesApp.Services.Models;
using WineriesApp.Services.Services;

namespace WineriesApp.Api.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewController : Controller
{
    private readonly IReviewService reviewService;

    public ReviewController(IReviewService reviewService)
    {
        this.reviewService = reviewService;
    }

    [HttpGet("{type}/{entityId}")]
    public async Task<IEnumerable<ReviewInfo>> GetReviews(ReviewEntityType type, Guid entityId)
    {
        var reviews = new List<Review>();

        switch (type)
        {
            case ReviewEntityType.Winery:
                reviews = await reviewService.GetWineryReviews(entityId);
                break;
            case ReviewEntityType.Wine:
                reviews = await reviewService.GetWineReviews(entityId);
                break;
            default:
                return new List<ReviewInfo>();
        }

        return reviews.Select(r => new ReviewInfo().CopyFromEntity(r));
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddReview(AddReview model)
    {
        if (!Enum.IsDefined(typeof(ReviewEntityType), model.EntityType))
        {
            return NotFound();
        }

        await reviewService.AddReview(model);

        return Ok();
    }
}