using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BitzArt.CA;

public static class AppInitExtension
{
    public static void Init(this IHost host, Action<IServiceScope> action)
    {
        var sw = Stopwatch.StartNew();
        using var scope = host.Services.CreateScope();
        action.Invoke(scope);
        sw.Stop();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<IHost>>();
        logger.LogInformation("App initialization completed in {ms} ms", sw.ElapsedMilliseconds);
    }
}
