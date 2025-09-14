using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class AuditablesInterceptor : OnSaveInterceptorBase
{
    protected sealed override void OnSave(DbContext dbContext)
    {
        var now = DateTimeOffset.UtcNow;

        var insertedEntries = dbContext.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Added)
            .Select(x => x.Entity);

        foreach (var entry in insertedEntries)
        {
            if (entry is not ICreatedAt simpleAuditable) continue;
            simpleAuditable.CreatedAt = now;

            if (entry is not IAuditable auditable) continue;
            auditable.LastUpdatedAt = now;
        }

        var updatedEntries = dbContext.ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entry in updatedEntries)
        {
            if (entry is not IAuditable auditable) continue;
            auditable.LastUpdatedAt = now;
        }
    }
}
