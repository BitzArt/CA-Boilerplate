using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BitzArt.CA;

public static class AppInitExtension
{
    public static void Init<T>(this IHost host, Func<IServiceScope, T> action)
    {
        using var scope = host.Services.CreateScope();
        action.Invoke(scope);
    }
}
