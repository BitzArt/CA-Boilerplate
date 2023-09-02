using BitzArt.CA.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace BitzArt.CA.Persistence.Tests;

[Collection("Service Collection")]
public class RepositoryTests
{
    private readonly TestServiceContainer _services;

    private readonly TestRepository _repository;

    public RepositoryTests(TestServiceContainer container)
    {
        _services = container;

        _repository = _services.GetRequiredService<TestRepository>();
    }

    [Fact]
    public async Task SaveChangesAsync_OnTestRepository_TracksActivity()
    {
        using var activity = _services.StartActivity();

        await _repository.SaveChangesAsync();
    }
}