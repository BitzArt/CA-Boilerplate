namespace BitzArt.CA;

/// <summary>
/// Default <see cref="ISoftDeletable{T}"/> deletion metadata.
/// </summary>
public class DeletionInfo
{
    /// <summary>
    /// The timestamp for when this object was marked as deleted.
    /// </summary>
    public DateTimeOffset? DeletedAt { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletionInfo"/> class with the specified deletion timestamp.
    /// </summary>
    /// <param name="deletedAt">
    /// Entity deletion timestamp. If <see langword="null"/>,
    /// <see cref="DateTimeOffset.UtcNow"/> will be used.
    /// </param>
    public DeletionInfo(DateTimeOffset? deletedAt = null)
    {
        deletedAt ??= DateTimeOffset.UtcNow;

        DeletedAt = deletedAt;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletionInfo"/> class.
    /// </summary>
    public DeletionInfo() { }
}