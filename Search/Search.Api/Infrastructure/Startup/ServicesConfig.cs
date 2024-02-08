using Search.Services.Services;

namespace Search.Api.Infrastructure.Startup;

public static class ServicesConfig
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        return services.AddScoped<IIngestionService, IngestionService>();
    }
}