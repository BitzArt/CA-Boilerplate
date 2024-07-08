using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace BitzArt.CA.SampleApp;

internal static class AddTelemetryExtensions
{
    public static IHostApplicationBuilder AddTelemetry(this IHostApplicationBuilder builder)
    {
        var section = builder.Configuration.GetSection(TelemetryOptions.SectionName);
        var options = section.Get<TelemetryOptions>()!;

        if (!options.Enabled) return builder;

        const string serviceName = "BitzArt.CA.SampleApp";

        builder.Logging.AddOpenTelemetry(options =>
        {
            options
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName))
                .AddConsoleExporter();
        });

        builder.Services.AddOpenTelemetry()
              .ConfigureResource(resource => resource.AddService(serviceName))
              .WithTracing(tracing =>
                  {
                      tracing
                      .AddAspNetCoreInstrumentation();

                      if (options.LogRepositoryCommands)
                      {
                          tracing.AddSource("BitzArt.CA.Persistence.EntityFrameworkCore");
                      }

                      if (options.UseOtlpExporter)
                      {
                          tracing.AddOtlpExporter(cfg =>
                           {
                               cfg.Endpoint = new Uri(options.OtlpEndpoint!);
                           });
                      }

                      if (options.UseConsoleExporter)
                      {
                          tracing.AddConsoleExporter();
                      }
                  });

        return builder;
    }
}
