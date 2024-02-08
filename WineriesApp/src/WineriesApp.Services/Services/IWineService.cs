using WineriesApp.DataContext.Models;
using WineriesApp.Services.Models.Filters;

namespace WineriesApp.Services.Services
{
    public interface IWineService
    {
        Task<List<Wine>> FilterWines(WinesFilter filter, CancellationToken token = default);

        Task<Wine?> GetWine(Guid id);

        Task IngestWines(CancellationToken token = default);
    }
}