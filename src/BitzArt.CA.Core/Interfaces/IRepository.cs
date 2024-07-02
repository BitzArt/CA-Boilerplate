using BitzArt.Pagination;

namespace BitzArt.CA;

public interface IRepository
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity> : IRepository
    where TEntity : class
{
    public void Add(TEntity entity);

    public void AddRange(IEnumerable<TEntity> entities);

    public void Remove(TEntity entity);

    public void RemoveRange(IEnumerable<TEntity> entities);
    
    [Obsolete("Use an overload with Func<IQueryable<TEntity>, IQueryable<TResult>> instead")]
    public Task<IEnumerable<TEntity>> GetAllAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
    public Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    [Obsolete("Use an overload with Func<IQueryable<TEntity>, IQueryable<TResult>> instead")]
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
    public Task<PageResult<TResult>> GetPageAsync<TResult>(PageRequest pageRequest, Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    [Obsolete("Use an overload with Func<IQueryable<TEntity>, IQueryable<TResult>> instead")]
    public Task<TEntity?> GetAsync(IFilterSet<TEntity> filter, CancellationToken cancellationToken = default);
    public Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    [Obsolete("Use an overload with Func<IQueryable<TEntity>, IQueryable<TResult>> instead")]
    public Task<int> CountAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
    public Task<int> CountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    [Obsolete("Use an overload with Func<IQueryable<TEntity>, IQueryable<TResult>> instead")]
    public Task<long> LongCountAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
    public Task<long> LongCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    [Obsolete("Use an overload with Func<IQueryable<TEntity>, IQueryable<TResult>> instead")]
    public Task<bool> AnyAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
    public Task<bool> AnyAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);
}