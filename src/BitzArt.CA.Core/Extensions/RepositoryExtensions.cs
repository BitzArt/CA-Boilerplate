namespace BitzArt.CA;

/// <summary>
/// Extension methods for <see cref="IRepository{T}"/>.
/// </summary>
public static class RepositoryExtensions
{
    /// <inheritdoc cref="IRepository{T}.Remove(T)"/>
    public static void Remove<T>(this IRepository<T> repository, T entity, bool hardDelete = false)
        where T : class, IHardDeletable
    {
        if (hardDelete)
        {
            entity.HardDelete();
        }
        
        repository.Remove(entity);
    }
}
