using WineriesApp.Services.Services;

namespace WineriesApp.Api.Infrastructure.Startup;

public static class SearchConfig
{
    public static async Task<IServiceProvider> ConfigureSearch(this IServiceProvider provider)
    {
        var wineryService = provider.GetService<IWineryService>() ?? throw new ArgumentNullException(nameof(IWineryService));
        var wineService = provider.GetService<IWineService>() ?? throw new ArgumentNullException(nameof(IWineService));

        await wineryService.IngestWineries();
        await wineService.IngestWines();

        return provider;
    }
}