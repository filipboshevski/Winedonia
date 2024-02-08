using Microsoft.AspNetCore.Mvc;
using WineriesApp.DataContext.Models;
using WineriesApp.Services.Services;

namespace WineriesApp.Api.Controllers;

[ApiController]
[Route("api/municipalities")]
public class MunicipalityController : ControllerBase
{
    private readonly IMunicipalityService municipalityService;

    public MunicipalityController(IMunicipalityService municipalityService)
    {
        this.municipalityService = municipalityService;
    }

    [HttpGet]
    public async Task<IEnumerable<Municipality>> GetMunicipalities()
    {
        return await municipalityService.GetMunicipalities();
    }
}