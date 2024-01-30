using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence;

public static class AddRelationalAppDbContextExtension
{
    public static IServiceCollection AddRelationalAppDbContext<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        where TContext : RelationalAppDbContext
    {
        services.AddDbContext<TContext>(options);

        services.AddScoped<AppDbContext>(x => x.GetRequiredService<TContext>());

        return services;
    }
}
