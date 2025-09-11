
namespace BitzArt.CA.SampleApp.Core;

public class Book : IEntity<int?>, IAuditable, ISoftDeletable, IHardDeletable
{
    public int? Id { get; set; }

    public string? Title { get; set; }
    public string? Author { get; set; }

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? LastUpdatedAt { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsHardDeleted { get; set; }
}
