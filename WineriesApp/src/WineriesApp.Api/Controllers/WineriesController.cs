using Microsoft.AspNetCore.Mvc;
using WineriesApp.Services.Mappers;
using WineriesApp.Services.Models;
using WineriesApp.Services.Models.Filters;
using WineriesApp.Services.Services;

namespace WineriesApp.Api.Controllers
{
    [ApiController]
    [Route("api/wineries")]
    public class WineriesController : ControllerBase
    {
        private readonly IWineryService wineryService;

        public WineriesController(IWineryService wineryService)
        {
            this.wineryService = wineryService;
        }

        [HttpPost("filter/search")]
        public async Task<WinerySearchResult> GetWineriesBySearch([FromBody] WineriesFilter model)
        {
            var wineries = await wineryService.FilterWineries(model);

            return new WinerySearchResult
            {
                Wineries = wineries.Take(model.BatchSize ?? 20).Select(w => new WinerySearchInfo().CopyFromEntity(w)),
                LastBatch = wineries.Count < model.BatchSize + 1
            };
        }

        [HttpGet("top-wineries")]
        public async Task<IEnumerable<WineryPreviewInfo>> GetTopWineries()
        {
            var wineries = await wineryService.GetTopWineries();

            return wineries.Select(w => new WineryPreviewInfo().CopyFromEntity(w));
        }

        [HttpGet("{id}/details")]
        public async Task<WineryInfo?> GetWineryDetails(Guid id)
        {
            var winery = await wineryService.GetWinery(id);

            if (winery == null)
            {
                return null;
            }

            return new WineryInfo().CopyFromEntity(winery);
        }
    }
}
