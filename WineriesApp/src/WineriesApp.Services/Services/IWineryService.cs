using WineriesApp.DataContext.Models;
using WineriesApp.Services.Models;
using WineriesApp.Services.Models.Filters;

namespace WineriesApp.Services.Services
{
    public interface IWineryService
    {
        Task<List<Winery>> FilterWineries(WineriesFilter filter, CancellationToken token = default);

        Task<List<Winery>> GetTopWineries();

        Task<Winery?> GetWinery(Guid id);

        Task IngestWineries(CancellationToken token = default);
    }
}
