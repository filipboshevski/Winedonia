using WineriesApp.DataContext.Models;
using WineriesApp.Services.Models;

namespace WineriesApp.Services.Mappers;

public static class WineMapper
{
    public static WineSearchInfo CopyFromEntity(this WineSearchInfo model, Wine entity)
    {
        model.Id = entity.Id;
        model.ImageUrl = entity.PreviewImageUrl;
        model.Name = entity.Name;
        model.Rating = entity.Rating;
        model.Type = entity.Type;

        return model;
    }
    
    public static WineInfo CopyFromEntity(this WineInfo model, Wine entity)
    {
        model.Id = entity.Id;
        model.ImageUrl = entity.MainImageUrl;
        model.Description = entity.Description.Split("^split^").ToList();
        model.Name = entity.Name;
        model.Rating = entity.Rating;
        model.Type = entity.Type;
        model.Wineries = entity.Wineries.Select(w => new WineryPreviewInfo().CopyFromEntity(w)).ToList();

        return model;
    }
}