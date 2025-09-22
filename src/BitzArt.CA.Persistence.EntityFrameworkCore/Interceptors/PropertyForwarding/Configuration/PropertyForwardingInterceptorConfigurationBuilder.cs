using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class PropertyForwardingInterceptorConfigurationBuilder<TEntity>
    (PropertyForwardingInterceptor<TEntity> interceptor)
    : IPropertyForwardingInterceptorConfigurationBuilder<TEntity>
    where TEntity : class
{
    PropertyForwardingInterceptor<TEntity> IPropertyForwardingInterceptorConfigurationBuilder<TEntity>.Interceptor => interceptor;

    public IPropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, TProperty> From<TProperty>(Func<DbContext, TEntity, TProperty> originValueGetter)
        => new PropertyForwardingInterceptorConfigurationRuleBuilder<TEntity, TProperty>(this, originValueGetter);
}
