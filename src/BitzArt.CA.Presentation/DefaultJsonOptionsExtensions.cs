using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BitzArt.CA;

public static class DefaultJsonOptionsExtensions
{
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
