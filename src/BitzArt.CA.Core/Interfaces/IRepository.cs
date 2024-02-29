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
    public void Remove(TEntity entity);
    public Task<TEntity?> GetAsync(IFilterSet<TEntity> filter, CancellationToken cancellationToken = default);
    public Task<int> CountAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
    public Task<bool> AnyAsync(IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, IFilterSet<TEntity>? filter = null, CancellationToken cancellationToken = default);
}