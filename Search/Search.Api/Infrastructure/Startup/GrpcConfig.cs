namespace Search.Api.Infrastructure.Startup;

public static class GrpcConfig
{
    public static IServiceCollection ConfigureGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }
}