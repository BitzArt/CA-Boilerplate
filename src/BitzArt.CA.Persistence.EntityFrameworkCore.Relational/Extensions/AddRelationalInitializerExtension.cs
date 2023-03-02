using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

public static class AddRelationalInitializerExtension
{
    public static IServiceCollection AddRelationalInitializer(this IServiceCollection services, bool allowDebug)
    {
        if (allowDebug) services.AddTransient<IAppDbInitializingService, DebugAppDbInitializingService<AppDbContext>>();
        else services.AddTransient<IAppDbInitializingService, AppDbInitializingService<AppDbContext>>();

        services.AddTransient<IPersistenceInitializationService, PersistenceInitializationService>();

        return services;
    }
}
