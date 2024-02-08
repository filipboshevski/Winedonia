namespace WineriesApp.Services.Models
{
    public class WinerySearchInfo : BaseWineryInfo
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }
        
        public string Address { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}