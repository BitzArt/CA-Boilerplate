using BitzArt.Pagination;

namespace BitzArt.CA;

/// <inheritdoc cref="IRepository{TEntity}"/>
public interface IRepository
{
    /// <summary>
    /// Represents the data provider used by the repository, typically an instance of a database context or similar.
    /// </summary>
    public object Provider { get; }

    /// <summary>
    /// Asynchronously saves all changes made in the context to the underlying data store.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous save operation.</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Defines a contract for a repository that provides controlled access to aggregate roots,
/// encapsulating data retrieval and persistence logic while maintaining separation between
/// the domain model and the data access layer.
/// </summary>
/// <typeparam name="TEntity">Entity type that represents the aggregate root.</typeparam>
public interface IRepository<TEntity> : IRepository
    where TEntity : class
{
    /// <summary>
    /// Marks the specified entity for addition to the repository.
    /// </summary>
    /// <param name="entity">Entity to add.</param>
    public void Add(TEntity entity);

    /// <summary>
    /// Marks a collection of entities for addition to the repository.
    /// </summary>
    /// <param name="entities">Collection of entities to add.</param>
    public void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Marks the specified entity for removal from the repository.
    /// </summary>
    /// <param name="entity">Entity to remove.</param>
    public void Remove(TEntity entity);

    /// <summary>
    /// Marks a collection of entities for removal from the repository.
    /// </summary>
    /// <param name="entities">Collection of entities to remove.</param>
    public void RemoveRange(IEnumerable<TEntity> entities);

    /// <inheritdoc cref="GetAllAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetAllAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<IEnumerable<object>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all records of type <typeparamref name="TEntity"/> from the repository.
    /// </summary>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="GetMapAsync{TResult, TKey}(Func{IQueryable{TEntity}, IQueryable{TResult}}, Func{TResult, TKey}, CancellationToken)"/>
    public Task<Dictionary<TKey, TEntity>> GetMapAsync<TKey>(Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull;

    /// <summary>
    /// Retrieves all records of type <typeparamref name="TResult"/> from the repository
    /// and maps them to a dictionary using the specified key selector function.
    /// </summary>
    /// <typeparam name="TResult">Resulting record type.</typeparam>
    /// <typeparam name="TKey">Key type for the dictionary.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="keySelector">Dictionary key selector function.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<Dictionary<TKey, TResult>> GetMapAsync<TResult, TKey>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, Func<TResult, TKey> keySelector, CancellationToken cancellationToken = default)
        where TKey : notnull;

    /// <inheritdoc cref="GetPageAsync{TPageRequest, TResult}(TPageRequest, Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<PageResult<TEntity, TPageRequest>> GetPageAsync<TPageRequest>(TPageRequest pageRequest, CancellationToken cancellationToken = default)
        where TPageRequest : IPageRequest;

    /// <inheritdoc cref="GetPageAsync{TPageRequest, TResult}(TPageRequest, Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<PageResult<object, TPageRequest>> GetPageAsync<TPageRequest>(TPageRequest pageRequest, Func<IQueryable<TEntity>, IQueryable> filter, CancellationToken cancellationToken = default)
        where TPageRequest : IPageRequest;

    /// <summary>
    /// Retrieves a page of records of type <typeparamref name="TResult"/> from the repository.
    /// </summary>
    /// <typeparam name="TPageRequest">Page request type that implements <see cref="IPageRequest"/>.</typeparam>
    /// <typeparam name="TResult">Resulting record type.</typeparam>
    /// <param name="pageRequest">Page request.</param>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<PageResult<TResult, TPageRequest>> GetPageAsync<TPageRequest, TResult>(TPageRequest pageRequest, Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default)
        where TPageRequest : IPageRequest;

    /// <inheritdoc cref="GetAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    [Obsolete("Use FirstOrDefaultAsync instead.", false)]
    public Task<TEntity?> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// <para>
    /// Retrieves a single record of type <typeparamref name="TEntity"/> from the repository.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">Resulting record type.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    [Obsolete("Use FirstOrDefaultAsync instead.", false)]
    public Task<TResult?> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="FirstAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<TEntity> FirstAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="FirstAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<object> FirstAsync(Func<IQueryable<TEntity>, IQueryable> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the first matching record of type <typeparamref name="TResult"/> from the repository.
    /// </summary>
    /// <typeparam name="TResult">Resulting type.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResult> FirstAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="FirstOrDefaultAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="FirstOrDefaultAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<object?> FirstOrDefaultAsync(Func<IQueryable<TEntity>, IQueryable> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the first matching record of type <typeparamref name="TResult"/> from the repository,
    /// or <see langword="null"/> if no matching record is found.
    /// </summary>
    /// <typeparam name="TResult">Resulting type.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResult?> FirstOrDefaultAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="SingleAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<TEntity> SingleAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="SingleAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<object> SingleAsync(Func<IQueryable<TEntity>, IQueryable> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para>
    /// Retrieves a single matching record of type <typeparamref name="TResult"/> from the repository.
    /// </para>
    /// <para>
    /// If more than one record matches the filter, an exception will be thrown.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">Resulting type.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResult> SingleAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="SingleOrDefaultAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<TEntity?> SingleOrDefaultAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="SingleOrDefaultAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<object?> SingleOrDefaultAsync(Func<IQueryable<TEntity>, IQueryable> filter, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para>
    /// Retrieves the single matching record of type <typeparamref name="TResult"/> from the repository,
    /// or <see langword="null"/> if no matching record is found.
    /// </para>
    /// <para>
    /// If more than one record matches the filter, an exception will be thrown.
    /// </para>
    /// </summary>
    /// <typeparam name="TResult">Resulting type.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<TResult?> SingleOrDefaultAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="CountAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<int> CountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Counts the number of matching records in the repository.
    /// </summary>
    /// <typeparam name="TResult">Resulting record type.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<int> CountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="LongCountAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<long> LongCountAsync(CancellationToken cancellationToken = default);

    /// <inheritdoc cref="CountAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<long> LongCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="AnyAsync{TResult}(Func{IQueryable{TEntity}, IQueryable{TResult}}, CancellationToken)"/>
    public Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if any records of type <typeparamref name="TResult"/> match the specified filter.
    /// </summary>
    /// <typeparam name="TResult">Resulting record type.</typeparam>
    /// <param name="filter">Filter function that transforms the queryable set of entities into a queryable set of results.</param>
    /// <param name="cancellationToken">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task<bool> AnyAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> filter, CancellationToken cancellationToken = default);
}