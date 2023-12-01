namespace BitzArt.CA.Persistence.Tests;

public class AuditableTests(AppDbContext db)
{
    [Fact]
    public void CreatedAt_OnSaveChanges_SavesCreatedTimestamp()
    {
        var now = DateTimeOffset.UtcNow;

        var entity = new TestCreatedAt()
        {
            Name = "created"
        };

        db.Add(entity);
        db.SaveChanges();

        Assert.NotNull(entity.CreatedAt);

        var createdAt = entity.CreatedAt;
        var diff = createdAt - now;
        Assert.True(diff < TimeSpan.FromSeconds(1));

        entity.Name = "updated";
        db.SaveChanges();

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

        db.Add(entity);
        db.SaveChanges();

        Assert.NotNull(entity.CreatedAt);
        Assert.NotNull(entity.LastUpdatedAt);

        var createdAt = entity.CreatedAt;
        Assert.True(createdAt - now < TimeSpan.FromSeconds(1));
        var updatedAt = entity.LastUpdatedAt;
        Assert.True(updatedAt - now < TimeSpan.FromSeconds(1));

        entity.Name = "updated";
        db.SaveChanges();

        Assert.NotNull(entity.CreatedAt);
        Assert.Equal(createdAt, entity.CreatedAt);
        Assert.NotNull(entity.LastUpdatedAt);
        Assert.NotEqual(updatedAt, entity.LastUpdatedAt);
    }
}