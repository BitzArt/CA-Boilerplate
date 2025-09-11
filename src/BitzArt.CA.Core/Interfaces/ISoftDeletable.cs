namespace BitzArt.CA;

/// <summary>
/// An object that can be soft-deleted.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Indicates whether this object is marked as deleted.
    /// </summary>
    public bool? IsDeleted { get; set; }
}

/// <summary>
/// An object that can be soft-deleted and includes deletion metadata.
/// </summary>
public interface ISoftDeletable<TDeletionInfo> : ISoftDeletable
    where TDeletionInfo : class
{
    /// <summary>
    /// Object deletion metadata.
    /// </summary>
    public TDeletionInfo? DeletionInfo { get; set; }
}
