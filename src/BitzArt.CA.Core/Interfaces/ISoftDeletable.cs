namespace BitzArt.CA;

/// <summary>
/// <para>
/// An object that can be soft-deleted.
/// </para>
/// <para>
/// This interface provides a flag indicating whether the object has been marked as deleted, 
/// along with a timestamp for when the deletion occurred.
/// </para>
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Indicates whether this object is marked as deleted.
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The timestamp when this object was marked as deleted.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }
}