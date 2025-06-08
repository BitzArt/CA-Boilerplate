namespace BitzArt.CA.Persistence;

/// <summary>
/// Service for initializing the persistence layer.
/// </summary>
public interface IPersistenceInitializationService
{
    /// <summary>
    /// Initializes the persistence layer.
    /// </summary>
    /// <param name="ct">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task InitAsync(CancellationToken ct = default);
}
