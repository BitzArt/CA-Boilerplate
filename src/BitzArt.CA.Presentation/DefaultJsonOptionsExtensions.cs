using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitzArt.CA;

/// <summary>
/// Extension methods for configuring default JSON options.
/// </summary>
public static class DefaultJsonOptionsExtensions
{
    /// <summary>
    /// Configured default JSON options for ReadFromJsonAsync and WriteAsJsonAsync.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to configure.</param>
    /// <returns><see cref="IServiceCollection"/> to allow chaining.</returns>"/>
    public static IServiceCollection ConfigureDefaultHttpJsonOptions(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions
            .PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

            options.SerializerOptions
                .DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            options.SerializerOptions
            .Converters.Add(new JsonStringEnumMemberConverter());
        });

        return services;
    }

    /// <summary>
    /// Configures the default JSON options for an MVC application.
    /// </summary>
    /// <param name="builder"><see cref="IMvcBuilder"/> to configure.</param>
    /// <returns><see cref="IMvcBuilder"/> to allow chaining.</returns>
    public static IMvcBuilder AddDefaultJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
         {
             options.JsonSerializerOptions
             .PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

             options.JsonSerializerOptions
             .DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

             options.JsonSerializerOptions
             .Converters.Add(new JsonStringEnumMemberConverter());
         });

        return builder;
    }
}
