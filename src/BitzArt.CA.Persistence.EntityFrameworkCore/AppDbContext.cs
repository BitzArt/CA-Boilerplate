using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

public abstract class AppDbContext : DbContext
{
    protected AppDbContext(DbContextOptions options) : base(options) { }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        UpdateAuditable();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

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

        foreach (var entry in insertedEntries)
        {
            if (entry is not IAuditable auditable) continue;
            auditable.UpdatedAt = DateTimeOffset.UtcNow;

            if (entry is not ICreatedAt simpleAuditable) continue;
            simpleAuditable.CreatedAt = DateTimeOffset.UtcNow;
        }

        var updatedEntries = ChangeTracker.Entries()
            .Where(x => x.State == EntityState.Modified)
            .Select(x => x.Entity);

        foreach (var entry in updatedEntries)
        {
            if (entry is not IAuditable auditable) continue;
            auditable.UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
