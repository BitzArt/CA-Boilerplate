using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BitzArt.CA.Persistence;

public class AppDbHealthcheck(AppDbContext db) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var ok = await db.Database.CanConnectAsync(cancellationToken);
        if (ok) return HealthCheckResult.Healthy();
        else return HealthCheckResult.Unhealthy();
    }
}
