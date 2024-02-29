using BitzArt.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BitzArt.CA.Persistence;

public abstract class AppDbRepository : IRepository
{
    protected readonly AppDbContext Db;

    protected AppDbRepository(AppDbContext db)
    {
        Db = db;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{GetType().Name}: SaveChanges");

        return await Db.SaveChangesAsync(cancellationToken);
    }
}

public class AppDbRepository<TEntity> : AppDbRepository, IRepository<TEntity>
    where TEntity : class
{
    protected AppDbRepository(AppDbContext db) : base(db) { }

    public void Add(TEntity entity) => Db.Add(entity);
    public void Remove(TEntity entity) => Db.Remove(entity);

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

public class AppDbRepository<TEntity, TKey> : AppDbRepository<TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected AppDbRepository(AppDbContext db) : base(db) { }

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
