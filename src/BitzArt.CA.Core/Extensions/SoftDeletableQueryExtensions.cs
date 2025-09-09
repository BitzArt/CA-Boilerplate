namespace BitzArt.CA;

/// <summary>
/// Extension methods for querying <see cref="ISoftDeletable"/> entities.
/// </summary>
public static class SoftDeletableQueryExtensions
{
    /// <summary>
    /// Filters the query to exclude entities that are marked as deleted.
    /// </summary>
    /// <typeparam name="T">The entity type that implements <see cref="ISoftDeletable"/>.</typeparam>
    /// <param name="query">The queryable collection of entities.</param>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> containing only entities that are not marked as deleted.
    /// </returns>
    public static IQueryable<T> ExceptDeleted<T>(this IQueryable<T> query) where T : ISoftDeletable
        => query.Where(x => x.IsDeleted != true);
    
    /// <summary>
    /// Filters the query to include only entities that are marked as deleted.
    /// </summary>
    /// <typeparam name="T">The entity type that implements <see cref="ISoftDeletable"/>.</typeparam>
    /// <param name="query">The queryable collection of entities.</param>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> containing only entities that are marked as deleted.
    /// </returns>
    public static IQueryable<T> OnlyDeleted<T>(this IQueryable<T> query) where T : ISoftDeletable
        => query.Where(x => x.IsDeleted == true);
}
