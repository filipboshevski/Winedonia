using WineriesApp.DataContext.Enums;

namespace WineriesApp.Services.Models;

public class AddReview
{
    public double Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public ReviewEntityType EntityType { get; set; }
    
    public Guid EntityId { get; set; }
}