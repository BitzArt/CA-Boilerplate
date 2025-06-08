namespace BitzArt;

/// <summary>
/// An object defined primarily by its identity.
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IEntity<TKey>
{
    /// <summary>
    /// Entity unique identifier.
    /// </summary>
    public TKey Id { get; set; }
}