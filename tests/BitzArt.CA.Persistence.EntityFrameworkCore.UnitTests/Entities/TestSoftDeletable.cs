namespace BitzArt.CA.Persistence;

public class TestSoftDeletable : ISoftDeletable
{
    public Guid? Id { get; set; }

    public bool? IsDeleted { get; set; }
}
