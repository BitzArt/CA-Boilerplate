using BitzArt.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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
            .StartActivity($"{GetType().Name}: SaveChanges");

        return await Db.SaveChangesAsync();
    }
}

public abstract class RepositoryBase<TEntity> : RepositoryBase, IRepository<TEntity>
    where TEntity : class
{
    protected RepositoryBase(AppDbContext db) : base(db) { }

    public void Add(TEntity entity) => Db.Add(entity);
    public void Remove(TEntity entity) => Db.Remove(entity);

    protected virtual IQueryable<TEntity> Set(IFilterSet<TEntity>? filter = null)
    {
        var result = Db.Set<TEntity>() as IQueryable<TEntity>;
        if (filter is not null) result = result.Apply(filter);

        return result;
    }

    public virtual async Task<TEntity?> GetAsync(IFilterSet<TEntity> filter)
    {
        return await Set(filter)
            .FirstOrDefaultAsync();
    }

    public virtual async Task<int> CountAsync(IFilterSet<TEntity>? filter = null)
    {
        return await Set(filter)
            .CountAsync();
    }

    public virtual async Task<bool> AnyAsync(IFilterSet<TEntity>? filter = null)
    {
        return await Set(filter)
            .AnyAsync();
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, IFilterSet<TEntity>? filter = null)
    {
        return await Set(filter)
            .ToPageAsync(pageRequest);
    }
}

public abstract class RepositoryBase<TEntity, TKey> : RepositoryBase<TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected RepositoryBase(AppDbContext db) : base(db) { }

    protected override IQueryable<TEntity> Set(IFilterSet<TEntity>? filter = null)
    {
        var result = Db.Set<TEntity>() as IQueryable<TEntity>;

        // Default behavior: order by Id,
        result = result.OrderBy(x => x.Id);

        // Default ordering may be overridden when applying the filter
        if (filter is not null) result = result.Apply(filter);

        return result;
    }
}
