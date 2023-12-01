namespace BitzArt.CA.Persistence;

public interface IAuditable : ICreatedAt
{
    public DateTimeOffset? LastUpdatedAt { get; set; }
}
