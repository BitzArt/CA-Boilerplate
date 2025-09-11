using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BitzArt.CA.Persistence;

internal class DeletablesInterceptor : ISaveChangesInterceptor
{
    /// <inheritdoc/>
    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var dbContext = eventData.Context!;

        var toHardDelete = dbContext.ChangeTracker
            .Entries<IHardDeletable>()
            .Where(e => e.Entity.IsHardDeleted == true);

        foreach (var entry in toHardDelete)
        {
            entry.State = EntityState.Deleted;
        }

        var toSoftDelete = dbContext.ChangeTracker
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
                entry.State = EntityState.Unchanged;
                continue;
            }

            entity.IsDeleted = true;

            if (entity is ISoftDeletable<DeletionInfo> entityWithDefaultDeletionInfo)
            {
                entityWithDefaultDeletionInfo.DeletionInfo = new(now);
            }
        }

        return new(result);
    }
}
