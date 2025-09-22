using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

/// <summary>
/// An interceptor built using directives for handling entities of type <typeparamref name="TEntity"/>.
/// </summary>
public sealed class DirectiveInterceptor<TEntity> : OnSaveInterceptorBase
    where TEntity : class
{
    private readonly List<Action<DbContext, TEntity>> _directives = [];

    /// <summary>
    /// Creates a new instance of <see cref="DirectiveInterceptor{TEntity}"/>.
    /// </summary>
    /// <param name="configure"></param>
    public DirectiveInterceptor(Action<IDirectiveInterceptorConfigurationBuilder<TEntity>> configure)
    {
        var configurationBuilder = new DirectiveInterceptorConfigurationBuilder<TEntity>(this);
        configure.Invoke(configurationBuilder);
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
            ApplyDirectives(dbContext, entity);
        }
    }

    private void ApplyDirectives(DbContext dbContext, TEntity entity)
    {
        foreach (var rule in _directives)
        {
            rule.Invoke(dbContext, entity);
        }
    }

    internal void AddDirective(Action<DbContext, TEntity> rule)
    {
        _directives.Add(rule);
    }
}
