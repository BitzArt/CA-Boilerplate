using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BitzArt.CA.Persistence;

public class DebugAppDbInitializingService<TContext> : AppDbInitializingService<TContext>
    where TContext : DbContext
{
    public DebugAppDbInitializingService(TContext db, ILogger<AppDbInitializingService<TContext>> logger)
        : base(db, logger) { }


    protected override async Task MigrateAsync(TContext db, CancellationToken ct)
    {
        try
        {
            Logger.LogWarning("Database initializer running with Debug operations allowed!");

            await db.Database.MigrateAsync(ct);
        }
        catch (Exception exception)
        {
            Logger.LogWarning("An exception occured while attempting to initialize database. Message: {message}", exception.Message);

            Logger.LogWarning("Resetting database. All data will be cleared!");

            await db.Database.EnsureDeletedAsync(ct);

            Logger.LogWarning("Database cleared successfully.");

            await db.Database.MigrateAsync(ct);

            Logger.LogWarning("DEBUG: Database re-created successfully.");
        }
    }
}
