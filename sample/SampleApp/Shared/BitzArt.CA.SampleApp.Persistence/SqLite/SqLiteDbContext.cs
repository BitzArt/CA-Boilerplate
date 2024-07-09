using BitzArt.CA.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.SampleApp.Persistence;

public class SqLiteDbContext(DbContextOptions<SqLiteDbContext> options)
    : RelationalAppDbContext<IDatabaseConfigurationPointer>(options)
{
}
