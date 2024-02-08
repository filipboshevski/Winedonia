using Search.Api.Protos;
using WineriesApp.Services.Clients;

namespace WineriesApp.Api.Infrastructure.Startup;

public static class GrpcClientsConfig
{
    public static IServiceCollection ConfigureGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<IngestionV1.IngestionV1Client>(o =>
        {
            var address = configuration.GetValue<string>("GrpcClients:Search:Address") ??
                          throw new ArgumentNullException("GrpcClient uri should not be null");
            o.Address = new Uri(address);
        });

        services.AddTransient<IIngestionClient, IngestionClient>();
        
        return services;
    }
}