namespace BitzArt.CA;

/// <summary>
/// Indicates that the entity supports hard deletion.
/// </summary>
public interface IHardDeletable : ISoftDeletable
{
    /// <summary>
    /// When <see langword="true"/>, the entity has been marked for hard deletion.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Setting this property to <see langword="true"/> indicates that
    /// the entity should be permanently removed from the repository during the next save operation.
    /// </para>
    /// <para>
    /// This property is not persisted in the repository
    /// and is used solely to signal the intention of hard deletion
    /// to the data access layer.
    /// </para>
    /// </remarks>
    public bool IsHardDeleted { get; set; }
}
