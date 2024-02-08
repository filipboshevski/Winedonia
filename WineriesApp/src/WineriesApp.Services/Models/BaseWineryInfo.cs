namespace WineriesApp.Services.Models
{
    public class BaseWineryInfo
    {
        public Guid Id { get; set; }
    
        public double Rating { get; set; }
    
        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}