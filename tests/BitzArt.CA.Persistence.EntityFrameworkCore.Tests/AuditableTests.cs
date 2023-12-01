using BitzArt.CA.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.Persistence.Tests;

[Collection("Service Collection")]
public class AuditableTests
{
    private readonly TestServiceContainer _services;
    private readonly AppDbContext _db;

    public AuditableTests(TestServiceContainer container)
    {
        _services = container;
        _db = _services.GetRequiredService<AppDbContext>();
    }

    [Fact]
    public void CreatedAt_OnSaveChanges_SavesCreatedTimestamp()
    {
        var now = DateTimeOffset.UtcNow;

        var entity = new TestCreatedAt()
        {
            Name = "created"
        };

        _db.Add(entity);
        _db.SaveChanges();

        Assert.NotNull(entity.CreatedAt);

        var createdAt = entity.CreatedAt;
        Assert.True(createdAt - now < TimeSpan.FromMilliseconds(1));

        entity.Name = "updated";
        _db.SaveChanges();

        Assert.NotNull(entity.CreatedAt);
        Assert.Equal(createdAt, entity.CreatedAt);
    }

    [Fact]
    public void Auditable_OnSaveChanges_SavesTimestamps()
    {
        var now = DateTimeOffset.UtcNow;

        var entity = new TestAuditable()
        {
            Name = "created"
        };

        _db.Add(entity);
        _db.SaveChanges();

        Assert.NotNull(entity.CreatedAt);
        Assert.NotNull(entity.LastUpdatedAt);

        var createdAt = entity.CreatedAt;
        Assert.True(createdAt - now < TimeSpan.FromMilliseconds(1));
        var updatedAt = entity.LastUpdatedAt;
        Assert.True(updatedAt - now < TimeSpan.FromMilliseconds(1));

        entity.Name = "updated";
        _db.SaveChanges();

        Assert.NotNull(entity.CreatedAt);
        Assert.Equal(createdAt, entity.CreatedAt);
        Assert.NotNull(entity.LastUpdatedAt);
        Assert.NotEqual(updatedAt, entity.LastUpdatedAt);
    }
}