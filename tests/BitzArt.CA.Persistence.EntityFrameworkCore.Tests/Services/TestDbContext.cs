using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class TestDbContext : AppDbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }
}