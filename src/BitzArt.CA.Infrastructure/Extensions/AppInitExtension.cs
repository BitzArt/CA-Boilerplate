using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BitzArt.CA;

/// <summary>
/// Extension methods for initializing the application.
/// </summary>
public static class AppInitExtension
{
    /// <summary>
    /// Initializes the application by executing the provided action within a service scope.
    /// </summary>
    /// <param name="host"><see cref="IHost"/> to create an <see cref="IServiceScope"/> from.</param>
    /// <param name="action">Application initialization action to execute.</param>
    public static void Init(this IHost host, Action<IServiceScope> action)
    {
        using var scope = host.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("Microsoft.Hosting.Lifetime");

        logger.LogInformation("App initialization started...");
        var sw = Stopwatch.StartNew();
        action.Invoke(scope);
        sw.Stop();
        logger.LogInformation("App initialization completed in {ms} ms", sw.ElapsedMilliseconds);
    }
}
