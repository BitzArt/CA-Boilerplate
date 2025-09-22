namespace BitzArt.CA.Persistence;

public class TestHardDeletable : IHardDeletable
{
    public Guid? Id { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsHardDeleted { get; set; }
}
