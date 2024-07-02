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
    protected readonly string RepositoryName = $"AppDbRepository<{typeof(TEntity).Name}>";

    public override string ToString() => RepositoryName;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: SaveChanges");

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
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: GetAll");

        return await Set()
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: GetAll");

        return await Set(filter)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: GetPage");

        return await Set()
            .ToPageAsync(pageRequest, cancellationToken);
    }

    public virtual async Task<PageResult<TResult>> GetPageAsync<TResult>(PageRequest pageRequest, Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: GetPage");

        return await Set(filter)
            .ToPageAsync(pageRequest, cancellationToken);
    }

    public virtual async Task<TEntity?> GetAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: Get");

        return await Set()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: Get");

        return await Set(filter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: Count");

        return await Set()
            .CountAsync(cancellationToken);
    }

    public virtual async Task<int> CountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: Count");

        return await Set(filter)
            .CountAsync(cancellationToken);
    }

    public virtual async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: LongCount");

        return await Set()
            .LongCountAsync(cancellationToken);
    }

    public virtual async Task<long> LongCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: LongCount");

        return await Set(filter)
            .LongCountAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: Any");

        return await Set()
            .AnyAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = Activity.Current?.Source
            .StartActivity($"{RepositoryName}: Any");

        return await Set(filter)
            .AnyAsync(cancellationToken);
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
