using WineriesApp.DataContext.Models;

namespace WineriesApp.Services.Models
{
    public class WineryInfo : BaseWineryInfo
    {
        public List<string> Description { get; set; } = new();
        
        public float Latitude { get; set; }

        public float Longitude { get; set; }
        
        public string Address { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
