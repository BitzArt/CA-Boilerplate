using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.Persistence;

internal class TestAppDbContext : AppDbContext
{
    private readonly Guid _id;
    private readonly string _fileName;
    private readonly SqliteConnection _connection;

    public TestAppDbContext()
    {
        _id = Guid.NewGuid();
        _fileName = $"./BitzArt.CA.Persistence.EntityFrameworkCore.UnitTests.{_id}.db";

        _connection = new SqliteConnection($"Data Source={_fileName}");
        _connection.Open();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder
            .UseSqlite(_connection!, sqlite =>
                {
                    sqlite.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                })
            .AddBoilerplateInterceptors());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestAppDbContext).Assembly);
    }

    public override void Dispose()
    {
        base.Dispose();

        _connection.Close();
        SqliteConnection.ClearAllPools();

        if (File.Exists(_fileName))
        {
            File.Delete(_fileName);
        }
    }

    public static async Task<TestAppDbContext> PrepareAsync(bool createDeletables = false)
    {
        // Arrange
        var context = new TestAppDbContext();
        await context.Database.EnsureCreatedAsync();

        // Warmup
        _ = context.Model;

        if (createDeletables)
        {
            var softDeletable = new TestSoftDeletable();
            context.Add(softDeletable);

            var hardDeletable = new TestHardDeletable();
            context.Add(hardDeletable);
        }

        await context.SaveChangesAsync();

        context.ChangeTracker.Clear();

        return context;
    }
}
