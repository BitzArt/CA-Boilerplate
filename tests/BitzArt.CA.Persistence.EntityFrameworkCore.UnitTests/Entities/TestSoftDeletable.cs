namespace BitzArt.CA.Persistence;

internal class TestSoftDeletable : ISoftDeletable
{
    public Guid? Id { get; set; }

    public bool? IsDeleted { get; set; }
}
