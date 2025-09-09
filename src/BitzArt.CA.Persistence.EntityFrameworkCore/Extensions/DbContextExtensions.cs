using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA;

/// <summary>
/// Extension methods for <see cref="DbContext"/>.
/// </summary>
public static class DbContextExtensions
{
    /// <inheritdoc cref="DbContext.Remove(object)"/>
    public static void Remove(this DbContext context, object entity, bool hardDelete = false)
    {
        if (hardDelete)
        {
            if (entity is not IHardDeletable hardDeletable)
            {
                throw new InvalidOperationException($"The entity of type {entity.GetType().Name} does not implement {nameof(IHardDeletable)}.");
            }

            hardDeletable.HardDelete();
        }

        context.Remove(entity);
    }

    /// <inheritdoc cref="DbContext.Remove{TEntity}(TEntity)"/>
    public static void Remove<T>(this DbContext context, T entity, bool hardDelete = false)
        where T : IHardDeletable
    {
        if (hardDelete)
        {
            entity.HardDelete();
        }
        
        context.Remove(entity);
    }
}
