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
}
