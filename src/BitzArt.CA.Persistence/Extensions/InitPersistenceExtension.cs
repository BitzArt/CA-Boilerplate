using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Extension methods for initializing the persistence layer.
/// </summary>
public static class InitPersistenceExtension
{
    /// <summary>
    /// Initializes the persistence layer within the provided <see cref="IServiceScope"/>.
    /// </summary>
    /// <param name="scope"></param>
    public static void InitPersistence(this IServiceScope scope)
    {
        var service = scope.ServiceProvider.GetService<IPersistenceInitializationService>();
        service?.InitAsync().Wait();
    }
}
