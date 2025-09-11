namespace BitzArt.CA.Persistence;

internal class TestHardDeletable : IHardDeletable
{
    public Guid? Id { get; set; }

    public bool? IsDeleted { get; set; }

    public bool IsHardDeleted { get; set; }
}
