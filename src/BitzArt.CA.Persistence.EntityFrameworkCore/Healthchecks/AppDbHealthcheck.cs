using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Health check for the application database.
/// </summary>
/// <param name="db"><see cref="AppDbContext"/> instance to use for health check.</param>
public class AppDbHealthcheck(AppDbContext db) : IHealthCheck
{
    async Task<HealthCheckResult> IHealthCheck.CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken)
    {
        var ok = await db.Database.CanConnectAsync(cancellationToken);
        if (ok) return HealthCheckResult.Healthy();
        else return HealthCheckResult.Unhealthy();
    }
}
