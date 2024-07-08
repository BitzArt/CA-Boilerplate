using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BitzArt.CA.SampleApp.Persistence;

public static class AddPersistenceExtension
{
    public static IHostApplicationBuilder AddPersistence(this IHostApplicationBuilder builder)
    {
        builder.Services.AddPersistence();
        return builder;
    }

    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddSqLiteDbContext();
        services.AddRepositories();
    }
}
