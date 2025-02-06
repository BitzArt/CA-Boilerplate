using BitzArt.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BitzArt.CA.Persistence;

public abstract class AppDbRepository(AppDbContext db) : IRepository
{
    protected readonly AppDbContext Db = db;

    protected static ActivitySource ActivitySource = new("BitzArt.CA.Persistence.EntityFrameworkCore");

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource.StartActivity($"{GetType().Name}: SaveChanges");

        return await Db.SaveChangesAsync(cancellationToken);
    }

    public object Provider => Db;
}

public class AppDbRepository<TEntity>(AppDbContext db) : AppDbRepository(db), IRepository<TEntity>
    where TEntity : class
{
    protected readonly string RepositoryName = $"Repository<{typeof(TEntity).Name}>";

    public override string ToString() => RepositoryName;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"{RepositoryName}: SaveChanges");

        return await Db.SaveChangesAsync(cancellationToken);
    }

    public virtual void Add(TEntity entity) => Db.Add(entity);
    public virtual void AddRange(IEnumerable<TEntity> entities) => Db.AddRange(entities);
    public virtual void Remove(TEntity entity) => Db.Remove(entity);
    public virtual void RemoveRange(IEnumerable<TEntity> entities) => Db.RemoveRange(entities);

    protected virtual IQueryable<TResult> Set<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter)
    {
        return filter(Set());
    }

    protected virtual IQueryable<TEntity> Set()
    {
        return Db.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetAll<{typeof(TEntity).Name}>");

        return await Set()
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetAll<{typeof(TResult).Name}>");

        return await Set(filter)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<Dictionary<TKey, TEntity>> GetMapAsync<TKey>(Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetMap<{typeof(TEntity).Name}>");

        return await Set()
            .ToDictionaryAsync(keySelector, cancellationToken);
    }

    public virtual async Task<Dictionary<TKey, TResult>> GetMapAsync<TResult, TKey>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, Func<TResult, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetMap<{typeof(TResult).Name}>");

        return await Set(filter)
            .ToDictionaryAsync(keySelector, cancellationToken);
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetPage<{typeof(TEntity).Name}>");

        return await Set()
            .ToPageAsync(pageRequest, cancellationToken);
    }

    public virtual async Task<PageResult<TResult>> GetPageAsync<TResult>(PageRequest pageRequest, Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetPage<{typeof(TResult).Name}>");

        return await Set(filter)
            .ToPageAsync(pageRequest, cancellationToken);
    }

    public virtual async Task<TEntity?> GetAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TEntity).Name}>");

        return await Set()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TResult).Name}>");

        return await Set(filter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Count<{typeof(TEntity).Name}>");

        return await Set()
            .CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Count<{typeof(TResult).Name}>");

        return await Set(filter)
            .CountAsync(cancellationToken);
    }

    public virtual async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"LongCount<{typeof(TEntity).Name}>");

        return await Set()
            .LongCountAsync(cancellationToken);
    }

    public virtual async Task<long> LongCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"LongCount<{typeof(TResult).Name}>");

        return await Set(filter)
            .LongCountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Any<{typeof(TEntity).Name}>");

        return await Set()
            .AnyAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Any<{typeof(TResult).Name}>");

        return await Set(filter)
            .AnyAsync(cancellationToken);
    }

    public virtual async Task<long> SumAsync(Expression<Func<TEntity, long>> selector, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Sum<{typeof(TEntity).Name}>");

        return await Set()
            .SumAsync(selector, cancellationToken);
    }

    public virtual async Task<long> SumAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, Expression<Func<TResult, long>> selector, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Sum<{typeof(TResult).Name}>");

        return await Set(filter)
            .SumAsync(selector, cancellationToken);
    }

    public virtual async Task<int> SumAsync(Expression<Func<TEntity, int>> selector, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Sum<{typeof(TEntity).Name}>");

        return await Set()
            .SumAsync(selector, cancellationToken);
    }

    public virtual async Task<int> SumAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, Expression<Func<TResult, int>> selector, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Sum<{typeof(TResult).Name}>");

        return await Set(filter)
            .SumAsync(selector, cancellationToken);
    }
}

public class AppDbRepository<TEntity, TKey>(AppDbContext db) : AppDbRepository<TEntity>(db)
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected override IQueryable<TEntity> Set()
    {
        // Default order: Id, ascending
        return base.Set().OrderBy(x => x.Id);
    }
}
