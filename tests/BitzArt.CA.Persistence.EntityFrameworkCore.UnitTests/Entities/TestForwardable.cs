namespace BitzArt.CA.Persistence;

public class TestForwardable
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public string? ForwardedName { get; set; }

    public TestForwardable(string? name) : this()
    {
        Name = name;
    }

    public TestForwardable() { }
}
