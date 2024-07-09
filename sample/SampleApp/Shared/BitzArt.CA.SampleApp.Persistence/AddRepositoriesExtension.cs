using BitzArt.CA.SampleApp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.SampleApp.Persistence;

public static class AddRepositoriesExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
    }
}
