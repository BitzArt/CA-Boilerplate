using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

public abstract class RelationalAppDbContext : AppDbContext
{
    protected RelationalAppDbContext(DbContextOptions options) : base(options) { }
}

public abstract class RelationalAppDbContext<TConfigurationPointer> : RelationalAppDbContext
{
    public RelationalAppDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TConfigurationPointer).Assembly);
    }
}
