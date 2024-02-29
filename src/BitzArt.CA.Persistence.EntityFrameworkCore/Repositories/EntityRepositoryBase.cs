using BitzArt.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

public abstract class EntityRepositoryBase<TEntity, TKey> : RepositoryBase<TEntity>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected EntityRepositoryBase(AppDbContext db) : base(db) { }

    public virtual async Task<TEntity?> GetAsync(IFilterSet<TEntity> filter)
    {
        return await Set(filter)
            .FirstOrDefaultAsync();
    }

    public virtual async Task<PageResult<TEntity>> GetPageAsync(PageRequest pageRequest, IFilterSet<TEntity>? filter = null)
    {
        return await Set(filter)
            .OrderBy(x => x.Id)
            .ToPageAsync(pageRequest);
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
}
