using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BitzArt.CA.SampleApp.Persistence;

public static class AddPersistenceExtension
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddSqLiteDbContext();
        services.AddRepositories();
    }
}