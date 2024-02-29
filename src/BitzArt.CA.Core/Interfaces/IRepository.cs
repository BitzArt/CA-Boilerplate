using BitzArt.Pagination;

namespace BitzArt.CA;

public interface IRepository
{
    Task<int> SaveChangesAsync();
}

public interface IRepository<TEntity>
    where TEntity : class
{
    public void Add(TEntity entity);
    public void Remove(TEntity entity);
    public Task<TEntity?> GetAsync(IFilterSet<TEntity> filter);
    public Task<int> CountAsync(IFilterSet<TEntity>? filter = null);
    public Task<bool> AnyAsync(IFilterSet<TEntity>? filter = null);
    public Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, IFilterSet<TEntity>? filter = null);
}