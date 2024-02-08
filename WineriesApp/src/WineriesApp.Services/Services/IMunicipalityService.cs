using WineriesApp.DataContext.Models;

namespace WineriesApp.Services.Services;

public interface IMunicipalityService
{
    Task<List<Municipality>> GetMunicipalities();
    
    Task<Municipality?> GetMunicipality(Guid id);
}