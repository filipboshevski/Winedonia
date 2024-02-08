using Serilog;
using ILogger = Serilog.ILogger;

namespace Search.Api.Infrastructure.Startup;

public static class LoggerConfig
{
    public static IServiceCollection ConfigureLogging(this IServiceCollection services)
    {
        using var log = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        return services.AddSingleton<ILogger>(log);
    }
}