using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class PropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, TProperty>
    (IPropertyForwardingInterceptorConfigurationBuilder<TEntity> builder, Func<DbContext, TEntity, TProperty> originValueGetter)
    : IPropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, TProperty>
    where TEntity : class
{
    public IPropertyForwardingInterceptorConfigurationBuilder<TEntity> To(Action<DbContext, TEntity, TProperty> targetValueSetter)
    {
        builder.Interceptor.AddRule(new PropertyForwardingRule<TEntity, TProperty>(originValueGetter, targetValueSetter));
        return builder;
    }
}
