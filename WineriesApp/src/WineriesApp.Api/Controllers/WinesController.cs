using Microsoft.AspNetCore.Mvc;
using WineriesApp.Services.Clients;
using WineriesApp.Services.Enums;
using WineriesApp.Services.Mappers;
using WineriesApp.Services.Models;
using WineriesApp.Services.Models.Filters;
using WineriesApp.Services.Services;

namespace WineriesApp.Api.Controllers;

[ApiController]
[Route("api/wines")]
public class WinesController : ControllerBase
{
    private readonly IWineService wineService;
    private readonly IIngestionClient ingestionClient;

    public WinesController(IWineService wineService, IIngestionClient ingestionClient)
    {
        this.wineService = wineService;
        this.ingestionClient = ingestionClient;
    }
    
    [HttpPost("filter/search")]
    public async Task<IEnumerable<WineSearchInfo>> GetWinesBySearch([FromBody] WinesFilter model)
    {
        var wines = await wineService.FilterWines(model);

        return wines.Select(w => new WineSearchInfo().CopyFromEntity(w));
    }
    
    [HttpGet("{id}/details")]
    public async Task<WineInfo?> GetWineDetails(Guid id)
    {
        var wine = await wineService.GetWine(id);

        if (wine == null)
        {
            return null;
        }

        return new WineInfo().CopyFromEntity(wine);
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test(int batchIndex, int batchSize)
    {
        return Ok(await ingestionClient.FuzzySearch(EntityType.Winery, batchIndex: batchIndex, batchSize: batchSize));
    }
}