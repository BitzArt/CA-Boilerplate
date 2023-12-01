namespace BitzArt.CA;

public interface IAuditable : ICreatedAt
{
    public DateTimeOffset? LastUpdatedAt { get; set; }
}
