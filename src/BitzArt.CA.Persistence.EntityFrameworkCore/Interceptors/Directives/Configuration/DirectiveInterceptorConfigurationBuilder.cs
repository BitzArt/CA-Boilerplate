using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class DirectiveInterceptorConfigurationBuilder<TEntity>
    (DirectiveInterceptor<TEntity> interceptor)
    : IDirectiveInterceptorConfigurationBuilder<TEntity>
    where TEntity : class
{
    DirectiveInterceptor<TEntity> IDirectiveInterceptorConfigurationBuilder<TEntity>.Interceptor => interceptor;

    public IDirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty> Forward<TProperty>(Func<DbContext, TEntity, TProperty> originValueGetter)
        => new DirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty>(this, originValueGetter);

    public IDirectiveInterceptorConfigurationBuilder<TEntity> Invoke(Action<DbContext, TEntity> action)
    {
        interceptor.AddDirective(action);
        return this;
    }
}
