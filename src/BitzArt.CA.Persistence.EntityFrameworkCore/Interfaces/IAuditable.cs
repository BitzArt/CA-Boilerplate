namespace BitzArt.CA.Persistence;

public interface IAuditable : ICreatedAt
{
    public DateTimeOffset? UpdatedAt { get; set; }
}
