using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using Testcontainers.MsSql;

namespace MMAS.AS;

public static class UseTestContainerSqlServerExtension
{
    public static string UseTestContainerSqlServer(this IServiceCollection services)
    {
        var assemblyName = new StackTrace()
            .GetFrame(1)!
            .GetMethod()!
            .DeclaringType!
            .Assembly
            .GetName()
            .Name;

        var container = new MsSqlBuilder()
            .WithName($"{assemblyName}.SqlServer")
            .WithPassword("P@ssw0rd")
            .WithPortBinding(8081, true)
            .Build();

        container.StartAsync().Wait(20000);

        var connectionString = container
            .GetConnectionString()
            .SetDatabaseName("Test");

        return connectionString;
    }
}
