using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Application-wide <see cref="DbContext"/>.
/// </summary>
public abstract class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <inheritdoc/>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateAuditable();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <inheritdoc/>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateAuditable();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void UpdateAuditable()
    {
        var insertedEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity);

        DateTimeOffset now = DateTimeOffset.UtcNow;

        foreach (var entry in insertedEntries)
        {
            if (entry is not ICreatedAt simpleAuditable) continue;
            simpleAuditable.CreatedAt = now;

            if (entry is not IAuditable auditable) continue;
            auditable.LastUpdatedAt = now;
        }

        var updatedEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entry in updatedEntries)
        {
            if (entry is not IAuditable auditable) continue;
            auditable.LastUpdatedAt = now;
        }
    }
}
