namespace BitzArt.CA.Persistence;

public class PersistenceSettings
{
    public const string SectionName = "Persistence";

    public required string DbType { get; set; }
    public required string ConnectionString { get; set; }
    public bool AllowDebugOperations { get; set; } = false;
}
