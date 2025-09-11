using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BitzArt.CA.Persistence;

internal class AuditablesInterceptor : ISaveChangesInterceptor
{
    /// <inheritdoc/>
    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var now = DateTimeOffset.UtcNow;
        var dbContext = eventData.Context!;

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

        return new(result);
    }
}
