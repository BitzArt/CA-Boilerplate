using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BitzArt.CA.Persistence;

internal abstract class OnSaveInterceptorBase : ISaveChangesInterceptor
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

    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        OnSave(eventData.Context!);

        return result;
    }

    protected abstract void OnSave(DbContext dbContext);
}
