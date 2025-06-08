using System.Runtime.Serialization;

namespace BitzArt.CA.Persistence;

/// <summary>
/// Known database types.
/// </summary>
public enum DbType : byte
{
    /// <summary>
    /// Unknown database type.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Microsoft SQL Server.
    /// </summary>
    [EnumMember(Value = "mssql")]
    MsSql = 1,

    /// <summary>
    /// PostgreSQL.
    /// </summary>
    [EnumMember(Value = "postgres")]
    Postgres = 2,

    /// <summary>
    /// MySQL.
    /// </summary>
    [EnumMember(Value = "mysql")]
    Mysql = 3,

    /// <summary>
    /// SQLite.
    /// </summary>
    [EnumMember(Value = "sqlite")]
    SqLite = 4,

    /// <summary>
    /// Cosmos DB (Azure).
    /// </summary>
    [EnumMember(Value = "cosmosdb")]
    CosmosDb = 101,

    /// <summary>
    /// MongoDB.
    /// </summary>
    [EnumMember(Value = "mongodb")]
    MongoDb = 111
}

/// <summary>
/// Extension methods for <see cref="DbType"/>.
/// </summary>
public static class DbTypeExtensions
{
    private static byte Raw(this DbType type)
        => (byte)type;

    /// <summary>
    /// Checks if the database type is a relational database.
    /// </summary>
    /// <param name="type"><see cref="DbType"/> to check.</param>
    /// <returns><see langword="true"/> if the database type is relational; otherwise, <see langword="false"/>.</returns>
    public static bool IsRelational(this DbType type)
        => type.Raw() > 0 && type.Raw() < 100;
}
