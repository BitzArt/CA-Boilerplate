using BitzArt.Pagination;

namespace BitzArt.CA;

public interface IRepository
{
    public object Provider { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public interface IRepository<TEntity> : IRepository
    where TEntity : class
{
    public void Add(TEntity entity);

    public void AddRange(IEnumerable<TEntity> entities);

    public void Remove(TEntity entity);

    public void RemoveRange(IEnumerable<TEntity> entities);

    public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    public Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    public Task<Dictionary<TKey, TEntity>> GetMapAsync<TKey>(Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull;

    public Task<Dictionary<TKey, TResult>> GetMapAsync<TResult, TKey>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, Func<TResult, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull;

    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, CancellationToken cancellationToken = default);

    public Task<PageResult<TResult>> GetPageAsync<TResult>(PageRequest pageRequest, Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    public Task<TEntity?> GetAsync(CancellationToken cancellationToken = default);

    public Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    public Task<int> CountAsync(CancellationToken cancellationToken = default);

    public Task<int> CountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    public Task<long> LongCountAsync(CancellationToken cancellationToken = default);

    public Task<long> LongCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    public Task<bool> AnyAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);
}