using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BitzArt.CA.Persistence;

internal class DebugAppDbInitializingService<TContext>(TContext db, ILogger<AppDbInitializingService<TContext>> logger)
    : AppDbInitializingService<TContext>(db, logger)
    where TContext : DbContext
{
    public override async Task InitializeAsync(CancellationToken ct = default)
    {
        Logger.LogWarning("Database debug operations are allowed.");
        Logger.LogInformation("Skipping database connection check.");

        Logger.LogInformation("Attempting to initialize database...");

        await MigrateAsync(_db, ct);

        Logger.LogInformation("Database initialized successfully");
    }

    protected override async Task MigrateAsync(TContext db, CancellationToken ct)
    {


        var applied = await db.Database.GetAppliedMigrationsAsync(ct);
        var current = db.Database.GetMigrations();

        var unknown = applied.FirstOrDefault(x => !current.Contains(x));
        if (unknown is not null)
        {
            await ResetDatabaseAsync(db, unknown, ct);
            return;
        }

        await db.Database.MigrateAsync(ct);
    }

    private async Task ResetDatabaseAsync(TContext db, string unknown, CancellationToken ct)
    {
        Logger.LogCritical("Migration Conflict!");
        Logger.LogWarning("Found unknown applied migration: {unknown}", unknown);
        Logger.LogWarning("Resetting database. All data will be deleted!");

        await db.Database.EnsureDeletedAsync(ct);

        Logger.LogWarning("Database deleted successfully.");

        await db.Database.MigrateAsync(ct);

        Logger.LogWarning("Database re-created successfully.");
    }
}
