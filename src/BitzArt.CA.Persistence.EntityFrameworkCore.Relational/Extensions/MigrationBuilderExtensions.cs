using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Microsoft.EntityFrameworkCore.Migrations;

public static class MigrationBuilderExtensions
{
    /// <summary>
    /// Executes the SQL statement via sp_executesql which does not get validated until runtime.
    /// </summary>
    public static OperationBuilder<SqlOperation> ExecuteSql(this MigrationBuilder migrationBuilder, string sql) =>
        migrationBuilder.Sql($"EXEC sp_executesql N'{sql.Replace("'", "''")}'");
}
