using System.ComponentModel.DataAnnotations;

namespace BitzArt.CA.Persistence;

internal class TestRepository : RepositoryBase
{
    public TestRepository(AppDbContext db) : base(db)
    {
    }
}