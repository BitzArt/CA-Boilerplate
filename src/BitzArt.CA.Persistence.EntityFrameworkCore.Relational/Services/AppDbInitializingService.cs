using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BitzArt.CA.Persistence;

public class AppDbInitializingService<TContext> : IAppDbInitializingService
    where TContext : DbContext
{
    protected readonly TContext _db;
    protected readonly ILogger Logger;

    public AppDbInitializingService(TContext db, ILogger<AppDbInitializingService<TContext>> logger)
    {
        _db = db;
        Logger = logger;
    }

    public async Task InitializeAsync(CancellationToken ct = default)
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
