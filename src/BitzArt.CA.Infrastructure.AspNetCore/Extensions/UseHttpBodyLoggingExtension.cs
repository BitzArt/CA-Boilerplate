using Azureblue.ApplicationInsights.RequestLogging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA;

public static class UseHttpBodyLoggingExtension
{
    public static IApplicationBuilder UseHttpBodyLogging(this IApplicationBuilder app)
    {
        var logHttpBody = app.ApplicationServices.GetService<IConfiguration>()!.GetValue<bool>("LogHttpBody");
        if (logHttpBody) app.UseAppInsightsHttpBodyLogging();

        return app;
    }
}
