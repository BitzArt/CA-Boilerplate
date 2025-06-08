using BitzArt.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BitzArt.CA.Persistence;

/// <inheritdoc cref="AppDbRepository{TEntity}"/>
public abstract class AppDbRepository(AppDbContext db) : IRepository
{
    /// <summary>
    /// Database context used by the repository for data access operations.
    /// </summary>
    protected readonly AppDbContext Db = db;

    private protected static ActivitySource ActivitySource = new("BitzArt.CA.Persistence.EntityFrameworkCore");

    /// <summary>
    /// Saves all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource.StartActivity($"{GetType().Name}: SaveChanges");

        return await Db.SaveChangesAsync(cancellationToken);
    }

    object IRepository.Provider => Db;
}

/// <inheritdoc cref="AppDbRepository{TEntity, TKey}"/>
public class AppDbRepository<TEntity>(AppDbContext db) : AppDbRepository(db), IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Name of the repository, used for logging and diagnostics purposes.
    /// </summary>
    protected readonly string RepositoryName = $"Repository<{typeof(TEntity).Name}>";

    /// <inheritdoc/>
    public override string ToString() => RepositoryName;

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"{RepositoryName}: SaveChanges");

        return await Db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual void Add(TEntity entity) => Db.Add(entity);

    /// <inheritdoc/>
    public virtual void AddRange(IEnumerable<TEntity> entities) => Db.AddRange(entities);

    /// <inheritdoc/>
    public virtual void Remove(TEntity entity) => Db.Remove(entity);

    /// <inheritdoc/>
    public virtual void RemoveRange(IEnumerable<TEntity> entities) => Db.RemoveRange(entities);

    /// <inheritdoc cref="Set{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}})"/>
    protected virtual IQueryable<TEntity> Set() => Db.Set<TEntity>();

    /// <summary>
    /// Returns a queryable set of entities of type <typeparamref name="TEntity"/> with optional filtering.
    /// </summary>
    /// <typeparam name="TResult">Result type of the query.</typeparam>
    /// <param name="filter">Filter function to apply to the queryable set.</param>
    /// <returns>An <see cref="IQueryable{TResult}"/> representing the filtered set of entities.</returns>
    protected virtual IQueryable<TResult> Set<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter) => filter.Invoke(Set());

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetAll<{typeof(TEntity).Name}>");

        return await Set()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetAll<{typeof(TResult).Name}>");

        return await Set(filter)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<Dictionary<TKey, TEntity>> GetMapAsync<TKey>(Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetMap<{typeof(TEntity).Name}>");

        return await Set()
            .ToDictionaryAsync(keySelector, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<Dictionary<TKey, TResult>> GetMapAsync<TResult, TKey>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, Func<TResult, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetMap<{typeof(TResult).Name}>");

        return await Set(filter)
            .ToDictionaryAsync(keySelector, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<PageResult<TEntity, TPageRequest>> GetPageAsync<TPageRequest>(TPageRequest pageRequest, CancellationToken cancellationToken = default)
        where TPageRequest : IPageRequest
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetPage<{typeof(TEntity).Name}>");

        return await Set()
            .ToPageAsync(pageRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<PageResult<TResult, TPageRequest>> GetPageAsync<TPageRequest, TResult>(TPageRequest pageRequest, Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
        where TPageRequest : IPageRequest
    {
        using var saveActivity = ActivitySource?.StartActivity($"GetPage<{typeof(TResult).Name}>");

        return await Set(filter)
            .ToPageAsync(pageRequest, cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TEntity).Name}>");

        return await Set()
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TResult).Name}>");

        return await Set(filter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> FirstAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TEntity).Name}>");

        return await Set()
            .FirstAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResult> FirstAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TResult).Name}>");

        return await Set(filter)
            .FirstAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TEntity).Name}>");

        return await Set()
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResult?> FirstOrDefaultAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TResult).Name}>");

        return await Set(filter)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity> SingleAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TEntity).Name}>");

        return await Set()
            .SingleAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResult> SingleAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TResult).Name}>");

        return await Set(filter)
            .SingleAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TEntity?> SingleOrDefaultAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TEntity).Name}>");

        return await Set()
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<TResult?> SingleOrDefaultAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Get<{typeof(TResult).Name}>");

        return await Set(filter)
            .SingleOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Count<{typeof(TEntity).Name}>");

        return await Set()
            .CountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Count<{typeof(TResult).Name}>");

        return await Set(filter)
            .CountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<long> LongCountAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"LongCount<{typeof(TEntity).Name}>");

        return await Set()
            .LongCountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<long> LongCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"LongCount<{typeof(TResult).Name}>");

        return await Set(filter)
            .LongCountAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Any<{typeof(TEntity).Name}>");

        return await Set()
            .AnyAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
    {
        using var saveActivity = ActivitySource?.StartActivity($"Any<{typeof(TResult).Name}>");

        return await Set(filter)
            .AnyAsync(cancellationToken);
    }
}

/// <summary>
/// Generic repository for a specific entity type.
/// </summary>
/// <remarks>
/// This repository implementation uses the <see cref="AppDbContext"/> registered in the application's dependency injection container.
/// </remarks>
/// <typeparam name="TEntity">Repository entity type.</typeparam>
/// <typeparam name="TKey">The type of the entity's unique identifier.</typeparam>
/// <param name="db">The <see cref="AppDbContext"/> instance to use for database operations.</param>
public class AppDbRepository<TEntity, TKey>(AppDbContext db) : AppDbRepository<TEntity>(db)
    where TEntity : class, IEntity<TKey>
{
    /// <inheritdoc/>
    protected override IQueryable<TEntity> Set()
    {
        // Default order: Id, ascending
        return base.Set().OrderBy(x => x.Id);
    }
}
