using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Microsoft.EntityFrameworkCore.Migrations;

/// <summary>
/// Extension methods for <see cref="MigrationBuilder"/>.
/// </summary>
public static class MigrationBuilderExtensions
{
    /// <summary>
    /// Executes SQL statement in a way that makes the database engine only validate it at runtime (sp_executesql).
    /// </summary>
    public static OperationBuilder<SqlOperation> ExecuteSql(this MigrationBuilder migrationBuilder, string sql) =>
        migrationBuilder.Sql($"EXEC sp_executesql N'{sql.Replace("'", "''")}'");
}
