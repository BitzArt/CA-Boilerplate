namespace BitzArt.CA.Persistence;

public abstract class RepositoryBase : IRepository
{
    protected readonly AppDbContext Db;

    public RepositoryBase(AppDbContext db)
    {
        Db = db;
    }

    public async Task<int> SaveChangesAsync() => await Db.SaveChangesAsync();
}
