using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;
using OpenTelemetry;
using System.Diagnostics;
using System.Reflection;
using OpenTelemetry.Resources;
using MMAS.AS;

namespace BitzArt.CA.Persistence;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var msSqlConnectionString = services.UseTestContainerSqlServer();

        var assembly = Assembly.GetExecutingAssembly();
        var serviceName = assembly!.GetName().Name!;

        var tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddSource(serviceName)
            .ConfigureResource(resource => resource.AddService(serviceName))
            .AddConsoleExporter()
            .Build()!;

        var activitySource = new ActivitySource(serviceName);
        services.AddSingleton(activitySource);

        var sqliteConnection = new SqliteConnection("Filename=:memory:");
        sqliteConnection.Open();

        services.AddDbContext<TestDbContext>(options =>
        {
            options.UseSqlServer(msSqlConnectionString, x =>
            x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });
        services.AddScoped<AppDbContext>(x => x.GetRequiredService<TestDbContext>());

        services.AddScoped<TestRepository>();
    }

    public static void Configure(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // Ensure the test database is clean and ready to run unit tests on
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }
}
