using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BitzArt.CA;

public static class AppInitExtension
{
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
