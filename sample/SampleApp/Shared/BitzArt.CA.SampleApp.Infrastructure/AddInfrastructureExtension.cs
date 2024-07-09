using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BitzArt.CA.SampleApp.Infrastructure;

public static class AddInfrastructureExtension
{
    public static IHostApplicationBuilder AddAddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure();
        return builder;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddServices();
        return services;
    }
}
