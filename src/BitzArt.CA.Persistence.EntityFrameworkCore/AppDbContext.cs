using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Application-wide <see cref="DbContext"/>.
/// </summary>
public abstract class AppDbContext : DbContext
{
    /// <inheritdoc/>
    public AppDbContext(DbContextOptions options) : base(options) { }

    /// <inheritdoc/>
    public AppDbContext() : base() { }

    /// <inheritdoc/>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.AddInterceptors(
            [
                new DeletablesInterceptor(),
                new AuditablesInterceptor()
            ]);
}
