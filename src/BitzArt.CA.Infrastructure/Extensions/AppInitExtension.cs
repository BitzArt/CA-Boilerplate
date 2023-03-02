using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BitzArt.CA;

public static class AppInitExtension
{
    public static void Init(this IHost host, Action<IServiceScope> action)
    {
        using var scope = host.Services.CreateScope();
        action.Invoke(scope);
    }
}
