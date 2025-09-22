using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence.Tests;

public class ForwardableTests
{
    [Fact]
    public async Task PropertyForwardingInterceptor_TestForwardable_ShouldForward()
    {
        // Arrange
        var called = false;
        string? forwardedName = null;

        using var db = await TestAppDbContext.PrepareAsync(options =>
        {
            options.AddInterceptors(new PropertyForwardingInterceptor<TestForwardable>(forward =>
            {
                forward.From(x => x.Name).To((_, value) =>
                {
                    called = true;
                    forwardedName = value;
                });

                forward.From(x => x.Name).To(x => x.ForwardedName);
                forward.From(x => x.Name).To("ShadowName");
            }));
        });
        const string name = "some-name";
        var entity = new TestForwardable(name);

        // Act
        db.Add(entity);
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        Assert.True(called);
        Assert.Equal(name, forwardedName);

        entity = await db.Set<TestForwardable>().FirstAsync();
        Assert.Equal(name, entity.Name);
        Assert.Equal(name, entity.ForwardedName);

        var entry = db.Entry(entity);
        Assert.Equal(name, entry.Property<string>("ShadowName").CurrentValue);
    }
}