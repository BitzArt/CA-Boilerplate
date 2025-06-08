using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Relational <see cref="AppDbContext"/> base class.
/// </summary>
public abstract class RelationalAppDbContext(DbContextOptions options) : AppDbContext(options) { }

/// <summary>
/// Relational <see cref="AppDbContext"/> base class.
/// </summary>
/// <typeparam name="TConfigurationPointer">Type used to point to the assembly that contains entity configurations.</typeparam>
public abstract class RelationalAppDbContext<TConfigurationPointer>(DbContextOptions options) : RelationalAppDbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TConfigurationPointer).Assembly);
    }
}
