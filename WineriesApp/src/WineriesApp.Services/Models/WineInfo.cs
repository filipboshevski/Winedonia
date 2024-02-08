using WineriesApp.DataContext.Enums;
using WineriesApp.DataContext.Models;

namespace WineriesApp.Services.Models
{
    public class WineInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public WineType Type { get; set; }

        public double Rating { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public List<string> Description { get; set; } = new();

        public List<WineryPreviewInfo> Wineries { get; set; } = new();
    }
}
