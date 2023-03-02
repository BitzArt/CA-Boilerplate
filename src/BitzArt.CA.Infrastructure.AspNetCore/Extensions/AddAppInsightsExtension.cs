using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA;

public static class AddAppInsightsExtension
{
    public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration, bool enableLogging = false)
    {
        if (!configuration
            .GetSection("ApplicationInsights")
            .Exists()) return services;

        services.AddApplicationInsightsTelemetry();

        if (enableLogging) services.EnableSqlLogging();

        return services;
    }

    private static void EnableSqlLogging(this IServiceCollection services)
    {
        services
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
        {
            module.EnableSqlCommandTextInstrumentation = true;
        });
    }
}
