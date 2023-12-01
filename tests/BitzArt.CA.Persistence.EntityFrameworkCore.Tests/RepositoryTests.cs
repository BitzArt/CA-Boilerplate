using System.Diagnostics;

namespace BitzArt.CA.Persistence.Tests;

public class RepositoryTests(TestRepository repository, ActivitySource activitySource)
{
    [Fact]
    public async Task SaveChangesAsync_OnTestRepository_TracksActivity()
    {
        using var activity = activitySource.StartActivity("test-activity");

        await repository.SaveChangesAsync();
    }
}