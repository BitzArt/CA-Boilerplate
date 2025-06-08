namespace BitzArt.CA;

/// <summary>
/// An object that has a creation timestamp.
/// </summary>
public interface ICreatedAt
{
    /// <summary>
    /// Object creation timestamp.
    /// </summary>
    public DateTimeOffset? CreatedAt { get; set; }
}
