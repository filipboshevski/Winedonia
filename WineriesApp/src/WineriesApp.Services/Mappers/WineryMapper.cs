using WineriesApp.DataContext.Models;
using WineriesApp.Services.Models;

namespace WineriesApp.Services.Mappers;

public static class WineryMapper
{
    public static WineryPreviewInfo CopyFromEntity(this WineryPreviewInfo model, Winery entity)
    {
        model.Id = entity.Id;
        model.ImageUrl = entity.ImageUrl;
        model.Description = entity.Description.Split("^split^").ToList();
        model.Rating = entity.Rating;
        model.Name = entity.Name;

        return model;
    }
    
    public static WineryInfo CopyFromEntity(this WineryInfo model, Winery entity)
    {
        model.Id = entity.Id;
        model.Name = entity.Name;
        model.Description = entity.Description.Split("^split^").ToList();
        model.Address = entity.Address;
        model.ImageUrl = entity.ImageUrl;
        model.Rating = entity.Rating;
        model.Contact = entity.PhoneNumber;
        model.Url = entity.Website;
        model.Longitude = entity.Longitude;
        model.Latitude = entity.Latitude;

        return model;
    }
    
    public static WinerySearchInfo CopyFromEntity(this WinerySearchInfo model, Winery entity)
    {
        model.Id = entity.Id;
        model.Name = entity.Name;
        model.Address = entity.Address;
        model.ImageUrl = entity.ImageUrl;
        model.Rating = entity.Rating;
        model.Contact = entity.PhoneNumber;
        model.Url = entity.Website;
        model.Longitude = entity.Longitude;
        model.Latitude = entity.Latitude;

        return model;
    }
}