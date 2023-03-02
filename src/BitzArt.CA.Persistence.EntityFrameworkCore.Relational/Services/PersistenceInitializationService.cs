namespace BitzArt.CA.Persistence;

internal class PersistenceInitializationService : IPersistenceInitializationService
{
    private readonly IAppDbInitializingService _initService;

    public PersistenceInitializationService(IAppDbInitializingService initService)
    {
        _initService = initService;
    }

    public async Task InitAsync(CancellationToken ct = default)
    {
        await _initService.InitializeAsync(ct);
    }
}
