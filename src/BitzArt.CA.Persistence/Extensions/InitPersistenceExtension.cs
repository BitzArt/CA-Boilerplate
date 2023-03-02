using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

public static class InitPersistenceExtension
{
    public static void InitPersistence(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetService<IPersistenceInitializationService>();
        service?.InitAsync().Wait();
    }
}
