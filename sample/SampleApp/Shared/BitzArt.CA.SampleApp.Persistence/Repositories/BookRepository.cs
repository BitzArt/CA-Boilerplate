using BitzArt.CA.Persistence;
using BitzArt.CA.SampleApp.Core;

namespace BitzArt.CA.SampleApp.Persistence;

internal class BookRepository(AppDbContext db) : AppDbRepository<Book, int>(db), IBookRepository
{
}
