using WineriesApp.DataContext.Models;
using WineriesApp.Services.Models;

namespace WineriesApp.Services.Mappers;

public static class ReviewMapper
{
    public static ReviewInfo CopyFromEntity(this ReviewInfo model, Review entity)
    {
        model.Rating = entity.Rating;
        model.Date = entity.Date;
        model.Comment = entity.Comment;

        return model;
    }
}