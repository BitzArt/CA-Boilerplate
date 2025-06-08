using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Extension methods for configuring a <see cref="RelationalAppDbContext"/> to <see cref="IServiceCollection"/>.
/// </summary>
public static class AddRelationalAppDbContextExtension
{
    /// <summary>
    /// Configures the <see cref="AppDbContext"/> with a <see cref="RelationalAppDbContext"/> in the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TContext">Type of the <see cref="RelationalAppDbContext"/> to use.</typeparam>
    /// <param name="services"><see cref="IServiceCollection"/> to configure <see cref="AppDbContext"/> for.</param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection AddRelationalAppDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        where TContext : RelationalAppDbContext
    {
        services.AddDbContext<TContext>(options);

        services.AddScoped<AppDbContext>(x => x.GetRequiredService<TContext>());

        return services;
    }
}
