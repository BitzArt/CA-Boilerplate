using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Extension methods for adding relational database initializers to the service collection.
/// </summary>
public static class AddRelationalInitializerExtension
{
    /// <summary>
    /// Configures the <see cref="IAppDbInitializingService"/> in the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to configure.</param>
    /// <param name="allowDebug">Whether to allow debug operations.</param>
    /// <returns></returns>
    public static IServiceCollection AddRelationalInitializer(this IServiceCollection services, bool allowDebug)
    {
        if (allowDebug) services.AddTransient<IAppDbInitializingService, DebugAppDbInitializingService<AppDbContext>>();
        else services.AddTransient<IAppDbInitializingService, AppDbInitializingService<AppDbContext>>();

        services.AddTransient<IPersistenceInitializationService, PersistenceInitializationService>();

        return services;
    }
}
