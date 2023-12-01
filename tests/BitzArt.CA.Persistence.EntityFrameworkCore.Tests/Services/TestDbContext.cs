using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class TestDbContext : AppDbContext
{
    public DbSet<TestCreatedAt> CreatedAt { get; set; }
    public DbSet<TestAuditable> Auditable { get; set; }

    public TestDbContext(DbContextOptions options) : base(options)
    {
    }
}