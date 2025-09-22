using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Builder for configuring property forwarding rules.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IPropertyForwardingInterceptorConfigurationBuilder<TEntity>
    where TEntity : class
{
    internal PropertyForwardingInterceptor<TEntity> Interceptor { get; }

    /// <inheritdoc/>
    public IPropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, object> From(string propertyName)
        => From<object>(propertyName);

    /// <summary>
    /// Specifies the source property from which the value will be forwarded.
    /// </summary>
    /// <typeparam name="TProperty">Property type.</typeparam>
    /// <param name="propertyName">Name of the property to get the value from.</param>
    /// <returns><see cref="IPropertyForwardingInterceptorConfigurationRuleBuilder{TEntity, TProperty}"/> for further configuration.</returns>
    public IPropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, TProperty> From<TProperty>(string propertyName)
        => From((dbContext, entity) =>
        {
            var entry = dbContext.Entry(entity);
            var property = entry.Property<TProperty>(propertyName);
            return property.CurrentValue;
        });

    /// <inheritdoc cref="From{TProperty}(Func{DbContext, TEntity, TProperty})"/>
    public IPropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, TProperty> From<TProperty>(Func<TEntity, TProperty> originValueGetter)
        => From((_, entity) => originValueGetter(entity));

    /// <summary>
    /// Specifies the source property from which the value will be forwarded.
    /// </summary>
    /// <typeparam name="TProperty">Property type.</typeparam>
    /// <param name="originValueGetter">Function to get the value from the source property.</param>
    /// <returns><see cref="IPropertyForwardingInterceptorConfigurationRuleBuilder{TEntity, TProperty}"/> for further configuration.</returns>
    public IPropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, TProperty> From<TProperty>(Func<DbContext, TEntity, TProperty> originValueGetter);
}
