using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence.Tests;

public class DeletableTests
{
    [Fact]
    public async Task SaveChanges_SoftDeletableEntityUnchanged_ShouldKeepUnchanged()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync(createDeletables: true);

        var entity = await db.Set<TestSoftDeletable>()
            .FirstAsync();

        // Act
        await db.SaveChangesAsync();

        // Assert
        var entry = db.Entry(entity);
        Assert.Equal(EntityState.Unchanged, entry.State);
        Assert.False(entity.IsDeleted);
    }

    [Fact]
    public async Task SaveChanges_SoftDeletableEntityRemoved_ShouldSoftDelete()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync(createDeletables: true);

        var entity = await db.Set<TestSoftDeletable>()
            .FirstAsync();

        // Act
        db.Remove(entity);
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        entity = await db.Set<TestSoftDeletable>()
            .IgnoreQueryFilters()
            .FirstAsync();

        Assert.True(entity.IsDeleted);
    }

    [Fact]
    public async Task SaveChanges_HardDeletableEntityRemoved_ShouldSoftDelete()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync(createDeletables: true);

        var entity = await db.Set<TestHardDeletable>()
            .FirstAsync();

        // Act
        db.Remove(entity);
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        entity = await db.Set<TestHardDeletable>()
            .IgnoreQueryFilters()
            .FirstAsync();

        Assert.True(entity.IsDeleted);
    }

    [Fact]
    public async Task SaveChanges_HardDeletableEntityHardRemovedViaDbContext_ShouldHardDelete()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync(createDeletables: true);

        var entity = await db.Set<TestHardDeletable>()
            .FirstAsync();

        // Act
        db.Remove(entity, true);
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        entity = await db.Set<TestHardDeletable>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        Assert.Null(entity);
    }

    [Fact]
    public async Task SaveChanges_HardDeletableEntityHardRemovedViaProperty_ShouldHardDelete()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync(createDeletables: true);

        var entity = await db.Set<TestHardDeletable>()
            .FirstAsync();

        // Act
        entity.IsHardDeleted = true;
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        entity = await db.Set<TestHardDeletable>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        Assert.Null(entity);
    }

    [Fact]
    public async Task SaveChanges_HardDeletableEntityHardRemovedViaHardDeleteMethod_ShouldHardDelete()
    {
        // Arrange
        using var db = await TestAppDbContext.PrepareAsync(createDeletables: true);

        var entity = await db.Set<TestHardDeletable>()
            .FirstAsync();

        // Act
        entity.HardDelete();
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        entity = await db.Set<TestHardDeletable>()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();

        Assert.Null(entity);
    }
}
