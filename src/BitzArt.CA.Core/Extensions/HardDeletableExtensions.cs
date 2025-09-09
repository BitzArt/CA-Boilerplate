namespace BitzArt.CA;

/// <summary>
/// Extension methods for <see cref="IHardDeletable"/> entities.
/// </summary>
public static class HardDeletableExtensions
{
    /// <summary>
    /// Marks the entity implementing <see cref="IHardDeletable"/> as hard deleted.
    /// It will be permanently removed from the database during the next change save operation.
    /// </summary>
    /// <param name="entity"></param>
    public static void HardDelete(this IHardDeletable entity)
    {
        entity.IsHardDeleted = true;
    }
}