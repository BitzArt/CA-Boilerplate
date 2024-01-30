using Azureblue.ApplicationInsights.RequestLogging;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitzArt.CA;

public static class AddAppInsightsExtension
{
    public static IServiceCollection AddApplicationInsights(this IServiceCollection services, IConfiguration configuration, Action<BodyLoggerOptions>? configureBodyLogging = null)
    {
        if (!configuration.GetSection("ApplicationInsights").Exists()) return services;

        services.AddApplicationInsightsTelemetry();

        configureBodyLogging ??= _ => { };
        services.AddAppInsightsHttpBodyLogging(configureBodyLogging);

        services.AddLogging(x =>
        {
            x.AddApplicationInsights();
        });

        services
            .ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
            {
                module.EnableSqlCommandTextInstrumentation = true;
            });

        return services;
    }
}
