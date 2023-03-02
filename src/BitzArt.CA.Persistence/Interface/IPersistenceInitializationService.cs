namespace BitzArt.CA.Persistence;

public interface IPersistenceInitializationService
{
    Task InitAsync(CancellationToken ct = default);
}
