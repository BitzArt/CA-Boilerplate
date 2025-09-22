using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class DeletablesInterceptor : OnSaveInterceptorBase
{
    protected sealed override void OnSave(DbContext dbContext)
    {
        var now = DateTimeOffset.UtcNow;

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
                // The value was already true before this deletion attempt.

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
