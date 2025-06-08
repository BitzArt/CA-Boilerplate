using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BitzArt.CA.Persistence;

internal class AppDbInitializingService<TContext>(TContext db, ILogger<AppDbInitializingService<TContext>> logger)
    : IAppDbInitializingService
    where TContext : DbContext
{
    protected readonly TContext _db = db;
    protected readonly ILogger Logger = logger;

    public virtual async Task InitializeAsync(CancellationToken ct = default)
    {
        CheckDatabaseConnection(_db, 5000, ct);
        Logger.LogInformation("Attempting to initialize database...");

        await MigrateAsync(_db, ct);

        Logger.LogInformation("Database initialized successfully");
    }

    protected virtual async Task MigrateAsync(TContext db, CancellationToken ct)
    {
        await db.Database.MigrateAsync(ct);
    }

    private void CheckDatabaseConnection(TContext db, int timeout, CancellationToken ct)
    {
        var connected = db.Database.CanConnectAsync(ct).Wait(timeout, ct);
        const string errorMsg = "Unable to connect to database. Consider checking connection strings or something...";
        if (!connected) throw new Exception(errorMsg);
        Logger.LogInformation("Database connection established.");
    }
}
