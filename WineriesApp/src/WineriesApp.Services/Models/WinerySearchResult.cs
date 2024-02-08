namespace WineriesApp.Services.Models;

public class WinerySearchResult : Result
{
    public IEnumerable<WinerySearchInfo> Wineries { get; set; } = new List<WinerySearchInfo>();
}