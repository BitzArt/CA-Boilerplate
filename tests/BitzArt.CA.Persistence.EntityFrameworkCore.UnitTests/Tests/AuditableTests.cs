using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence.Tests;

public class AuditableTests
{
    private readonly TimeSpan _precision = TimeSpan.FromMilliseconds(100);

    [Fact]
    public async Task SaveChanges_Created_ShouldSetCreatedAtAndLastUpdatedAt()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync();

        var now = DateTimeOffset.UtcNow;
        var entity = new TestAuditable();

        // Act
        db.Add(entity);
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();
        entity = await db.Set<TestAuditable>().FirstAsync();

        Assert.NotNull(entity.CreatedAt);
        Assert.NotNull(entity.LastUpdatedAt);
        Assert.Equal(now, entity.CreatedAt!.Value, _precision);
        Assert.Equal(now, entity.LastUpdatedAt!.Value, _precision);
    }

    [Fact]
    public async Task SaveChanges_Updated_ShouldUpdateLastUpdatedAt()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync();

        var createdAt = DateTimeOffset.UtcNow;
        var entity = new TestAuditable("old name");

        db.Add(entity);
        await db.SaveChangesAsync();
        db.ChangeTracker.Clear();

        // Ensure time difference
        var delay = TimeSpan.FromMilliseconds(300);
        await Task.Delay(delay);

        // Act
        var updatedAt = DateTimeOffset.UtcNow;
        entity = await db.Set<TestAuditable>().FirstAsync();
        entity.Name = "new name";
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        entity = await db.Set<TestAuditable>().FirstAsync();
        Assert.Equal("new name", entity.Name);
        Assert.NotNull(entity.CreatedAt);
        Assert.True(updatedAt > createdAt + delay - _precision);
        Assert.Equal(createdAt, entity.CreatedAt!.Value, _precision);
        Assert.NotNull(entity.LastUpdatedAt);
        Assert.Equal(updatedAt, entity.LastUpdatedAt!.Value, _precision);
    }
}
