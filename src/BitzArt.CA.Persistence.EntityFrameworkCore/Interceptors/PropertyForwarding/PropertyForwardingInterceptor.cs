using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Interceptor that forwards property values from their source to their target destinations.
/// </summary>
public sealed class PropertyForwardingInterceptor<TEntity> : OnSaveInterceptorBase
    where TEntity : class
{
    private readonly List<PropertyForwardingRule<TEntity>> _rules = [];

    /// <summary>
    /// Creates a new instance of <see cref="PropertyForwardingInterceptor{TEntity}"/>.
    /// </summary>
    /// <param name="configure"></param>
    public PropertyForwardingInterceptor(Action<IPropertyForwardingInterceptorConfigurationBuilder<TEntity>> configure)
    {
        var builder = new PropertyForwardingInterceptorConfigurationBuilder<TEntity>(this);
        configure.Invoke(builder);
    }

    /// <inheritdoc/>
    protected override void OnSave(DbContext dbContext)
    {
        var entities = dbContext.ChangeTracker
            .Entries<TEntity>()
            .Where(e => e.State > EntityState.Unchanged)
            .Select(e => e.Entity);

        foreach (var entity in entities)
        {
            ForwardValues(dbContext, entity);
        }
    }

    private void ForwardValues(DbContext dbContext, TEntity entity)
    {
        foreach (var configuration in _rules)
        {
            configuration.ForwardValue(dbContext, entity);
        }
    }

    internal void AddRule(PropertyForwardingRule<TEntity> rule)
    {
        _rules.Add(rule);
    }
}
