using System.Diagnostics;
using System.Reflection;

namespace BitzArt.CA.Persistence;

public abstract class RepositoryBase : IRepository
{
    protected readonly AppDbContext Db;

    protected RepositoryBase(AppDbContext db)
    {
        Db = db;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{MethodBase.GetCurrentMethod()!.DeclaringType!.Name}: SaveChanges");
        
        return await Db.SaveChangesAsync();
    }
}

public abstract class RepositoryBase<TEntity> : RepositoryBase
    where TEntity : class
{
    protected RepositoryBase(AppDbContext db) : base(db) { }

    public void Add(TEntity entity) => Db.Add(entity);
    public void Remove(TEntity entity) => Db.Remove(entity);
}
