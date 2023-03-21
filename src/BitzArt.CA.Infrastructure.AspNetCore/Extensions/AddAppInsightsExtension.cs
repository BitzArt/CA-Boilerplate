using Azureblue.ApplicationInsights.RequestLogging;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA;

public static class AddAppInsightsExtension
{
    public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration, bool fullLogging = false)
    {
        if (!configuration
            .GetSection("ApplicationInsights")
            .Exists()) return services;

        services.AddApplicationInsightsTelemetry();

        if (fullLogging) services.AddFullLogging();

        return services;
    }

    private static void AddFullLogging(this IServiceCollection services)
    {
        services.AddAppInsightsHttpBodyLogging();

        services
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
        {
            module.EnableSqlCommandTextInstrumentation = true;
        });
    }
}
