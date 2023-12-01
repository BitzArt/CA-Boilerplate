using System.ComponentModel.DataAnnotations;

namespace BitzArt.CA.Persistence;

internal class TestCreatedAt : ICreatedAt
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
}

internal class TestAuditable : IAuditable
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }

    public DateTimeOffset? LastUpdatedAt { get; set; }
}
