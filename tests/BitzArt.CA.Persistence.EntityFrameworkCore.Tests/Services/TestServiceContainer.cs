using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit.Abstractions;

namespace BitzArt.CA.Persistence.Services;

[CollectionDefinition("Service Collection")]
public class ContainerCollection : ICollectionFixture<TestServiceContainer> { }

public class TestServiceContainer : IDisposable, IServiceProvider
{
    private readonly IServiceProvider _services;
    private readonly TracerProvider _tracerProvider;

    public object? GetService(Type serviceType) => _services.GetService(serviceType);
    public Activity? StartActivity([CallerMemberName] string callerMethodName = "") => _services.GetRequiredService<ActivitySource>().StartActivity(callerMethodName);

    public IServiceProvider ServiceProvider => _services.CreateScope().ServiceProvider;
	
	public TestServiceContainer()
	{
        var services = new ServiceCollection();

        var assembly = Assembly.GetExecutingAssembly();
        var serviceName = assembly!.GetName().Name!;

        _tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddSource(serviceName)
            .ConfigureResource(resource => resource.AddService(serviceName))
            .AddConsoleExporter()
            .Build()!;

        var activitySource = new ActivitySource(serviceName);
        services.AddSingleton(activitySource);

        var sqliteConnection = new SqliteConnection("Filename=:memory:");
        sqliteConnection.Open();

        services.AddDbContext<TestDbContext>(x => x.UseSqlite(sqliteConnection));
        services.AddScoped<AppDbContext>(x => x.GetRequiredService<TestDbContext>());

        services.AddScoped<TestRepository>();

        _services = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        _tracerProvider.Dispose();

        GC.SuppressFinalize(this);
    }
}
