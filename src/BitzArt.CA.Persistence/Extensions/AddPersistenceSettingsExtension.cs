using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Extension methods for configuring Persistence layer.
/// </summary>
public static class AddPersistenceSettingsExtension
{
    /// <summary>
    /// Configures Persistence layer in an <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static PersistenceSettings AddPersistenceSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration
            .GetRequiredSection(PersistenceSettings.SectionName)
            .Get<PersistenceSettings>()!;

        services.AddSingleton(settings);
        return settings;
    }
}
