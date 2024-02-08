using WineriesApp.DataContext.Enums;

namespace WineriesApp.Services.Models;

public class WineSearchInfo
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public WineType Type { get; set; }
    
    public double Rating { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}