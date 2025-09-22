namespace BitzArt.CA.Persistence;

public class TestAuditable : IAuditable
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? LastUpdatedAt { get; set; }

    public TestAuditable(string? name) : this()
    {
        Name = name;
    }

    public TestAuditable() { }
}
