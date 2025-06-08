namespace BitzArt.CA;

/// <summary>
/// <para>
/// An object that has a last update timestamp.
/// </para>
/// <para>
/// This interface extends <see cref="ICreatedAt"/> to include a timestamp for the last update.
/// </para>
/// </summary>
public interface IAuditable : ICreatedAt
{
    /// <summary>
    /// An object that has it's last update timestamp.
    /// </summary>
    public DateTimeOffset? LastUpdatedAt { get; set; }
}
