using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Base class for interceptors that need to perform actions before saving changes to the database.
/// </summary>
public abstract class OnSaveInterceptorBase : ISaveChangesInterceptor
{
    /// <inheritdoc/>
    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        OnSave(eventData.Context!);

        return new(result);
    }

    /// <inheritdoc/>
    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        OnSave(eventData.Context!);

        return result;
    }

    /// <summary>
    /// Called before changes are saved to the database.
    /// </summary>
    /// <param name="dbContext"></param>
    protected abstract void OnSave(DbContext dbContext);
}
