using BitzArt.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BitzArt.CA.Persistence;

public abstract class AppDbRepository(AppDbContext db) : IRepository
{
    protected readonly AppDbContext Db = db;

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{GetType().Name}: SaveChanges");

        return await Db.SaveChangesAsync(cancellationToken);
    }
}

public class AppDbRepository<TEntity>(AppDbContext db) : AppDbRepository(db), IRepository<TEntity>
    where TEntity : class
{
    public void Add(TEntity entity) => Db.Add(entity);
    public void AddRange(IEnumerable<TEntity> entities) => Db.AddRange(entities);
    public void Remove(TEntity entity) => Db.Remove(entity);
    public void RemoveRange(IEnumerable<TEntity> entities) => Db.RemoveRange(entities);

    protected virtual IQueryable<TEntity> Set(IFilterSet<TEntity>? filter = null)
    {
        var result = Db.Set<TEntity>() as IQueryable<TEntity>;
        if (filter is not null) result = result.Apply(filter);

        return result;
    }

    public virtual async Task<TEntity?> GetAsync(IFilterSet<TEntity> filter, CancellationToken cancellationToken = default)
    {
        return await Set(filter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default)
    {
        return await Set(filter)
            .CountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default)
    {
        return await Set(filter)
            .AnyAsync(cancellationToken);
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default)
    {
        return await Set(filter)
            .ToPageAsync(pageRequest, cancellationToken);
    }
}

public class AppDbRepository<TEntity, TKey>(AppDbContext db) : AppDbRepository<TEntity>(db)
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
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
