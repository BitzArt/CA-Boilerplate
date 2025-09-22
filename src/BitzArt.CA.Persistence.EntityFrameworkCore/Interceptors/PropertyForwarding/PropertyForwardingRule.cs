using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class PropertyForwardingRule<TEntity, TProperty> : PropertyForwardingRule<TEntity>
    where TEntity : class
{
    private readonly Func<DbContext, TEntity, TProperty> _originValueGetter;
    private readonly Action<DbContext, TEntity, TProperty> _targetValueSetter;

    public PropertyForwardingRule(
        Func<DbContext, TEntity, TProperty> originValueGetter,
        Action<DbContext, TEntity, TProperty> targetValueSetter)
    {
        _originValueGetter = originValueGetter;
        _targetValueSetter = targetValueSetter;
    }

    public override void ForwardValue(DbContext dbContext, TEntity entity)
    {
        var originValue = _originValueGetter(dbContext, entity);
        _targetValueSetter.Invoke(dbContext, entity, originValue);
    }
}

internal abstract class PropertyForwardingRule<TEntity>
    where TEntity : class
{
    public abstract void ForwardValue(DbContext dbContext, TEntity entity);
}
