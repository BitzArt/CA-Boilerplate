using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

public static class InitPersistenceExtension
{
    public static void InitPersistence(this IServiceScope scope)
    {
        var service = scope.ServiceProvider.GetService<IPersistenceInitializationService>();
        service?.InitAsync().Wait();
    }
}
