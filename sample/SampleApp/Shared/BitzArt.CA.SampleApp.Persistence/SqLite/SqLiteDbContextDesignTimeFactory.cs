using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BitzArt.CA.SampleApp.Persistence;

public class SqLiteDbContextDesignTimeFactory : IDesignTimeDbContextFactory<SqLiteDbContext>
{
    public SqLiteDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<SqLiteDbContext>();
        var sqliteConnection = new SqliteConnection("Data Source=../../../Sample.db");
        sqliteConnection.Open();

        builder.UseSqlite(sqliteConnection);

        return new SqLiteDbContext(builder.Options);
    }
}
