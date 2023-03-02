using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

public static class AddPersistenceExtension
{
    public static PersistenceSettings AddPersistenceSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetRequiredSection(PersistenceSettings.SectionName)
            .Get<PersistenceSettings>()!;

        services.AddSingleton(settings);
        return settings;
    }
}
