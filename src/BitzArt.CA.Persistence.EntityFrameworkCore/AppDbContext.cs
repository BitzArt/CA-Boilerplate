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
        OnSaveChanges();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <inheritdoc/>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnSaveChanges();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnSaveChanges()
    {
        var now = DateTimeOffset.UtcNow;

        HandleDeletables(now);
        HandleAuditables(now);
    }

    private void HandleAuditables(DateTimeOffset now)
    {
        var insertedEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity);

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

    private void HandleDeletables(DateTimeOffset now)
    {
        var toHardDelete = ChangeTracker
            .Entries<IHardDeletable>()
            .Where(e => e.Entity.IsHardDeleted == true);

        foreach (var entry in toHardDelete)
        {
            entry.State = EntityState.Deleted;
        }

        var toSoftDelete = ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        foreach (var entry in toSoftDelete)
        {
            var entity = entry.Entity;

            if (entity is IHardDeletable hardDeletable && hardDeletable.IsHardDeleted)
            {
                // Entity is marked for hard deletion,
                // so let it be deleted from the database.
                continue;
            }

            entry.State = EntityState.Modified;

            if (entity.IsDeleted == true)
            {
                // Already marked as deleted, no need to update again.
                // e.g. if the entity was loaded from the database with IsDeleted = true,
                // or the value was set explicitly before calling SaveChanges.

                // NOTE: We also do not update DeletionInfo in this case by design.
                continue;
            }

            entity.IsDeleted = true;
            
            if (entity is ISoftDeletable<DeletionInfo> entityWithDefaultDeletionInfo)
            {
                entityWithDefaultDeletionInfo.DeletionInfo = new(now);
            }
        }
    }
}
