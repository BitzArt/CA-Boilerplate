namespace BitzArt.CA.Persistence;

public interface IAppDbInitializingService
{
    Task InitializeAsync(CancellationToken ct = default);
}