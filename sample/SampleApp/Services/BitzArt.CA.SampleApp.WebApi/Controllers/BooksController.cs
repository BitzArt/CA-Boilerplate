using BitzArt.CA.SampleApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace BitzArt.CA.SampleApp;

[Route("books")]
public class BooksController(IBookRepository bookRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery(Name = "property")] string property)
    {
        var result = await bookRepository.GetAllAsync(q =>
        {
            // showcasing dynamic query projection
            if (string.Equals(property, "title", StringComparison.InvariantCultureIgnoreCase))
            {
                return q.Select(book => book.Title);
            }

            return q;
        });

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        var book = await bookRepository.FirstOrDefaultAsync(books => books.Where(x => x.Id == id))
            ?? throw new Exception($"Book with ID '{id}' was not found.");

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] Book book)
    {
        bookRepository.Add(book);
        await bookRepository.SaveChangesAsync();

        return Ok(book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] Book request)
    {
        var book = await bookRepository.FirstOrDefaultAsync(books => books.Where(x => x.Id == id))
            ?? throw new Exception($"Book with ID '{id}' was not found.");

        book.Title = request.Title;
        book.Author = request.Author;

        await bookRepository.SaveChangesAsync();

        return Ok(book);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var book = await bookRepository.FirstOrDefaultAsync(books => books.Where(x => x.Id == id))
            ?? throw new Exception($"Book with ID '{id}' was not found.");

        bookRepository.Remove(book);
        await bookRepository.SaveChangesAsync();

        return Ok();
    }
}
