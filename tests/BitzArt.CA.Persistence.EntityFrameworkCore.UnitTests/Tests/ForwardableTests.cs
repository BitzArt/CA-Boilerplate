using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence.Tests;

public class ForwardableTests
{
    [Fact]
    public async Task PropertyForwardingInterceptor_TestForwardable_ShouldForward()
    {
        // Arrange
        var invokeCalled = false;
        var forwardCalled = false;
        string? forwardedName = null;

        using var db = await TestAppDbContext.PrepareAsync(options =>
        {
            options.AddInterceptors(new DirectiveInterceptor<TestForwardable>(forward =>
            {
                forward.Invoke(_ => invokeCalled = true);

                forward.Forward(x => x.Name).To((_, value) =>
                {
                    forwardCalled = true;
                    forwardedName = value;
                });

                forward.Forward(x => x.Name).To(x => x.ForwardedName);
                forward.Forward(x => x.Name).To("ShadowName");
            }));
        });
        const string name = "some-name";
        var entity = new TestForwardable(name);

        // Act
        db.Add(entity);
        await db.SaveChangesAsync();

        // Assert
        db.ChangeTracker.Clear();

        Assert.True(invokeCalled);
        Assert.True(forwardCalled);
        Assert.Equal(name, forwardedName);

        entity = await db.Set<TestForwardable>().FirstAsync();
        Assert.Equal(name, entity.Name);
        Assert.Equal(name, entity.ForwardedName);

        var entry = db.Entry(entity);
        Assert.Equal(name, entry.Property<string>("ShadowName").CurrentValue);
    }
}