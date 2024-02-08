using Microsoft.EntityFrameworkCore;
using WineriesApp.DataContext;
using WineriesApp.DataContext.Models;

namespace WineriesApp.Services.Services;

public class MunicipalityService : IMunicipalityService
{
    private readonly WineriesDbContext context;

    public MunicipalityService(WineriesDbContext context)
    {
        this.context = context;
    }
    
    public Task<List<Municipality>> GetMunicipalities()
    {
        return context.Municipalities.ToListAsync();
    }

    public Task<Municipality?> GetMunicipality(Guid id)
    {
        return context.Municipalities.FirstOrDefaultAsync(m => m.Id == id);
    }
}