using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

public abstract class AppDbContext : DbContext
{
    protected AppDbContext(DbContextOptions options) : base(options) { }
}
