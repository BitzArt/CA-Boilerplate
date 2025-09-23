using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Builder for configuring property forwarding rules.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IDirectiveInterceptorConfigurationBuilder<TEntity>
    where TEntity : class
{
    internal DirectiveInterceptor<TEntity> Interceptor { get; }

    /// <inheritdoc/>
    public IDirectiveInterceptorForwardingRuleBuilder<TEntity, object> Forward(string propertyName)
        => Forward<object>(propertyName);

    /// <summary>
    /// Specifies the source property from which the value will be forwarded.
    /// </summary>
    /// <typeparam name="TProperty">Property type.</typeparam>
    /// <param name="propertyName">Name of the property to get the value from.</param>
    /// <returns><see cref="IDirectiveInterceptorForwardingRuleBuilder{TEntity, TProperty}"/> for further configuration.</returns>
    public IDirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty> Forward<TProperty>(string propertyName)
        => Forward((dbContext, entity) =>
        {
            var entry = dbContext.Entry(entity);
            var property = entry.Property<TProperty>(propertyName);
            return property.CurrentValue;
        });

    /// <inheritdoc cref="Forward{TProperty}(Func{DbContext, TEntity, TProperty})"/>
    public IDirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty> Forward<TProperty>(Func<TEntity, TProperty> originValueGetter)
        => Forward((_, entity) => originValueGetter(entity));

    /// <summary>
    /// Specifies the source property from which the value will be forwarded.
    /// </summary>
    /// <typeparam name="TProperty">Property type.</typeparam>
    /// <param name="originValueGetter">Function to get the value from the source property.</param>
    /// <returns><see cref="IDirectiveInterceptorForwardingRuleBuilder{TEntity, TProperty}"/> for further configuration.</returns>
    public IDirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty> Forward<TProperty>(Func<DbContext, TEntity, TProperty> originValueGetter);

    /// <inheritdoc cref="Invoke(Action{DbContext, TEntity})"/>
    public IDirectiveInterceptorConfigurationBuilder<TEntity> Invoke(Action<TEntity> action)
        => Invoke((_, entity) => action(entity));

    /// <summary>
    /// Adds a custom action to be executed for each entity of type <typeparamref name="TEntity"/> being saved or updated.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <returns><see cref="IDirectiveInterceptorConfigurationBuilder{TEntity}"/> to allow further configuration.</returns>
    public IDirectiveInterceptorConfigurationBuilder<TEntity> Invoke(Action<DbContext, TEntity> action);
}
