namespace BitzArt.CA;

/// <summary>
/// Extension methods for <see cref="ISoftDeletable"/> entities.
/// </summary>
public static class SoftDeletableExtensions
{
    /// <summary>
    /// Marks the entity implementing <see cref="ISoftDeletable"/> as deleted.
    /// </summary>
    /// <param name="entity">Entity to mark as deleted.</param>
    public static void SoftDelete(this ISoftDeletable entity)
    {
        if (entity.ImplementsGenericSoftDeletable(out var deletionInfoType))
        {
            var implementingTypeName = $"{nameof(ISoftDeletable)}<{deletionInfoType!.Name}>";

            throw new InvalidOperationException($"The entity implements {implementingTypeName}. " +
                $"Use {nameof(SoftDelete)}<{deletionInfoType.Name}>" +
                $"({implementingTypeName} entity, {deletionInfoType.Name} deletionInfo) overload instead.");
        }

        entity.IsDeleted = true;
    }

    /// <summary>
    /// Marks the entity implementing <see cref="ISoftDeletable{TDeletionInfo}"/> as deleted and sets the deletion info.
    /// </summary>
    /// <typeparam name="TDeletionInfo">Deletion info type.</typeparam>
    /// <param name="entity">Entity to mark as deleted.</param>
    /// <param name="deletionInfo">Deletion info to set.</param>
    public static void SoftDelete<TDeletionInfo>(this ISoftDeletable<TDeletionInfo> entity, TDeletionInfo deletionInfo)
        where TDeletionInfo : class
    {
        entity.IsDeleted = true;
        entity.DeletionInfo = deletionInfo;
    }

    /// <summary>
    /// Restores the entity implementing <see cref="ISoftDeletable"/> by marking it as no longer deleted.
    /// </summary>
    /// <param name="entity">Entity to restore.</param>
    public static void Restore(this ISoftDeletable entity)
    {
        if (entity.ImplementsGenericSoftDeletable(out var deletionInfoType))
        {
            var implementingTypeName = $"{nameof(ISoftDeletable)}<{deletionInfoType!.Name}>";

            throw new InvalidOperationException($"The entity implements {implementingTypeName}. " +
                $"Use {nameof(SoftDelete)}<{deletionInfoType.Name}>" +
                $"({implementingTypeName} entity, {deletionInfoType.Name} deletionInfo) overload instead.");
        }

        entity.IsDeleted = false;
    }

    /// <summary>
    /// Restores the entity implementing <see cref="ISoftDeletable{TDeletionInfo}"/> by marking it as no longer deleted.
    /// </summary>
    /// <typeparam name="TDeletionInfo">Deletion info type.</typeparam>
    /// <param name="entity">Entity to restore.</param>
    public static void Restore<TDeletionInfo>(this ISoftDeletable<TDeletionInfo> entity)
        where TDeletionInfo : class
    {
        entity.IsDeleted = false;
        entity.DeletionInfo = null;
    }

    internal static bool ImplementsGenericSoftDeletable(this ISoftDeletable entity, out Type? deletionInfoType)
    {
        deletionInfoType = null;

        var genericType = entity
            .GetType()
            .GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISoftDeletable<>));

        if (genericType is null)
        {
            return false;
        }

        deletionInfoType = genericType.GetGenericArguments()[0];
        return true;
    }
}
