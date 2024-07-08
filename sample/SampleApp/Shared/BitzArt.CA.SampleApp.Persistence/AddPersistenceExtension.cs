using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BitzArt.CA.Persistence;

namespace BitzArt.CA.SampleApp.Persistence;

public static class AddPersistenceExtension
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.Services.AddPersistence(builder.Configuration);
        return builder;
    }

    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = services.AddPersistenceSettings(configuration);
        services.AddSqLite(settings.ConnectionString);
        services.AddRepositories();
    }
}
