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
            await db.Database.MigrateAsync(ct);
        }
        catch (Exception)
        {
            Logger.LogInformation("An exception occured while attempting to initialize database.");

            Logger.LogInformation("DEBUG: Resetting database. All data will be cleared!");

            await db.Database.EnsureDeletedAsync(ct);

            Logger.LogInformation("DEBUG: Database cleared successfully.");

            await db.Database.MigrateAsync(ct);

            Logger.LogInformation("DEBUG: Database re-created successfully.");
        }
    }
}
