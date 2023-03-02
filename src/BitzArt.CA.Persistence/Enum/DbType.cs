using System.Runtime.Serialization;

namespace BitzArt.CA.Persistence;

public enum DbType : byte
{
    Unknown = 0,

    [EnumMember(Value = "mssql")]
    MsSql = 1,

    [EnumMember(Value = "postgres")]
    Postgres = 2,

    [EnumMember(Value = "mysql")]
    Mysql = 3,

    [EnumMember(Value = "cosmosdb")]
    CosmosDb = 101,

    [EnumMember(Value = "mongodb")]
    MongoDb = 111
}

public static class DbTypeExtensions
{
    private static byte Raw(this DbType type)
        => (byte)type;

    public static bool IsRelational(this DbType type)
        => type.Raw() > 0 && type.Raw() < 100;
}
