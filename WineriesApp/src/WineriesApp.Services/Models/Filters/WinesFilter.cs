using WineriesApp.DataContext.Enums;

namespace WineriesApp.Services.Models.Filters
{
    public class WinesFilter : BaseFilter
    {
        public string? SearchTerm { get; set; }
        
        public double[] Ratings { get; set; } = Array.Empty<double>();

        public WineType[] Types { get; set; } = Array.Empty<WineType>();
    }
}