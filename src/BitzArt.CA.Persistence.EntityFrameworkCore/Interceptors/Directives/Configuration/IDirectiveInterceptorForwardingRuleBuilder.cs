using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Builder for configuring a specific property forwarding rule.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TProperty">Property type.</typeparam>
public interface IDirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty>
    where TEntity : class
{
    /// <summary>
    /// Specifies the target property to which the value will be forwarded.
    /// </summary>
    /// <param name="propertyName">Name of the property to set the value to.</param>
    /// <returns><see cref="IDirectiveInterceptorConfigurationBuilder{TEntity}"/> to allow further configuration.</returns>
    public IDirectiveInterceptorConfigurationBuilder<TEntity> To(string propertyName)
        => To((dbContext, entity, value) =>
        {
            var entry = dbContext.Entry(entity);
            var property = entry.Property<TProperty>(propertyName);
            property.CurrentValue = value;
        });

    /// <summary>
    /// Specifies the target property to which the value will be forwarded.
    /// </summary>
    /// <param name="targetPropertyExpression">Expression to specify the target property.</param>
    /// <returns><see cref="IDirectiveInterceptorConfigurationBuilder{TEntity}"/> to allow further configuration.</returns>
    public IDirectiveInterceptorConfigurationBuilder<TEntity> To(Expression<Func<TEntity, TProperty>> targetPropertyExpression)
    {
        var memberExpression = targetPropertyExpression.Body as MemberExpression
            ?? throw new ArgumentException("The expression must be a member expression.", nameof(targetPropertyExpression));

        var memberInfo = memberExpression.Member;

        Action<TEntity, TProperty> setter = memberInfo switch
        {
            PropertyInfo propertyInfo => (entity, property) => propertyInfo.SetValue(entity, property),
            FieldInfo fieldInfo => (entity, property) => fieldInfo.SetValue(entity, property),
            _ => throw new ArgumentException("The member must be a field or property.", nameof(targetPropertyExpression))
        };

        return To(setter);
    }

    /// <inheritdoc cref="To(Action{DbContext, TEntity, TProperty})"/>
    public IDirectiveInterceptorConfigurationBuilder<TEntity> To(Action<TEntity, TProperty> targetValueSetter)
        => To((_, entity, value) => targetValueSetter(entity, value));

    /// <summary>
    /// Specifies the target property to which the value will be forwarded.
    /// </summary>
    /// <param name="targetValueSetter">Action to set the value to the target property.</param>
    /// <returns><see cref="IDirectiveInterceptorConfigurationBuilder{TEntity}"/> to allow further configuration.</returns>
    public IDirectiveInterceptorConfigurationBuilder<TEntity> To(Action<DbContext, TEntity, TProperty> targetValueSetter);
}
