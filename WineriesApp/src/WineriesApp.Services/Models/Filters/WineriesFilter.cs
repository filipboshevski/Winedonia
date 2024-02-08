namespace WineriesApp.Services.Models.Filters
{
    public class WineriesFilter : BaseFilter
    {
        public string? SearchTerm { get; set; } = null;
    
        public double[] Ratings { get; set; } = Array.Empty<double>();

        public Guid[] Locations { get; set; } = Array.Empty<Guid>();
    }
}