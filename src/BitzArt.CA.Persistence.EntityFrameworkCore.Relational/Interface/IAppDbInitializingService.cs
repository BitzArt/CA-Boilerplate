namespace BitzArt.CA.Persistence;

/// <summary>
/// Service for initializing the application database.
/// </summary>
public interface IAppDbInitializingService
{
    /// <summary>
    /// Initializes the application database.
    /// </summary>
    /// <param name="ct">Cancellation token that can be used to cancel the operation.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task InitializeAsync(CancellationToken ct = default);
}