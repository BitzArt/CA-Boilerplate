using BitzArt.CA.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BitzArt.CA.SampleApp.Persistence;

public static class AddSqLiteDbContextExtension
{
    public static void AddSqLiteDbContext(this IServiceCollection services)
    {
        services.AddRelationalAppDbContext<SqLiteDbContext>(options =>
        {
            var sqliteConnection = new SqliteConnection("Data Source=Sample.db");
            sqliteConnection.Open();

            options.UseSqlite(sqliteConnection,
                x => x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        });

        services.AddRelationalInitializer(true);
    }
}
