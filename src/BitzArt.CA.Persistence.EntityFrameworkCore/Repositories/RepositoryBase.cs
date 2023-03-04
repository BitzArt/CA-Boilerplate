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

public abstract class RepositoryBase<TEntity> : RepositoryBase
    where TEntity : class
{
    protected RepositoryBase(AppDbContext db) : base(db) { }

    public void Add(TEntity entity) => Db.Add(entity);
    public void Remove(TEntity entity) => Db.Remove(entity);
}
