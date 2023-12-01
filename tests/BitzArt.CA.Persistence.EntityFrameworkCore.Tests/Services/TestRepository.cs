using System.ComponentModel.DataAnnotations;

namespace BitzArt.CA.Persistence;

public class TestRepository : RepositoryBase
{
    public TestRepository(AppDbContext db) : base(db)
    {
    }
}