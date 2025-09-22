using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class DirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty>
    (IDirectiveInterceptorConfigurationBuilder<TEntity> builder, Func<DbContext, TEntity, TProperty> originValueGetter)
    : IDirectiveInterceptorForwardingRuleBuilder<TEntity, TProperty>
    where TEntity : class
{
    public IDirectiveInterceptorConfigurationBuilder<TEntity> To(Action<DbContext, TEntity, TProperty> targetValueSetter)
    {
        builder.Interceptor.AddDirective((dbContext, entity) =>
        {
            var value = originValueGetter.Invoke(dbContext, entity);
            targetValueSetter.Invoke(dbContext, entity, value);
        });
        return builder;
    }
}
