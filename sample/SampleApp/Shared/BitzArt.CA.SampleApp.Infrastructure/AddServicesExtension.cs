using BitzArt.CA.SampleApp.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.SampleApp.Infrastructure;

public static class AddServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITimeService, TimeService>();
    }
}
