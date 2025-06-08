namespace BitzArt.CA.Persistence;

/// <summary>
/// Persistence layer settings.
/// </summary>
public class PersistenceSettings
{
    /// <summary>
    /// Name of the configuration section.
    /// </summary>
    public const string SectionName = "Persistence";

    /// <summary>
    /// Database type.
    /// </summary>
    public string? DbType { get; set; }

    /// <summary>
    /// Database connection string.
    /// </summary>
    public required string ConnectionString { get; set; }

    /// <summary>
    /// Flag indicating whether debug operations are allowed.
    /// </summary>
    public bool AllowDebugOperations { get; set; } = false;
}
