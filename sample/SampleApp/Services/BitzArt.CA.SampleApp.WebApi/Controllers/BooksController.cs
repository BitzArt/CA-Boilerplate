using BitzArt.CA.SampleApp.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitzArt.CA.SampleApp;

[Route("books")]
public class BooksController(IRepository<Book> bookRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var books = await bookRepository.GetAllAsync();
        return Ok(books);
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
    public async Task<IActionResult> DeleteAsync(
        [FromRoute(Name = "id")] int id,
        [FromQuery(Name = "hard")] bool hardDelete = false)
    {
        var book = await bookRepository.FirstOrDefaultAsync(books => books.Where(x => x.Id == id))
            ?? throw new Exception($"Book with ID '{id}' was not found.");

        bookRepository.Remove(book, hardDelete);

        await bookRepository.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("{id:int}/restore")]
    public async Task<IActionResult> RestoreAsync([FromRoute(Name = "id")] int id)
    {
        var book = await bookRepository.FirstOrDefaultAsync(books => books
            .IgnoreQueryFilters()
            .DeletedOnly()
            .Where(x => x.Id == id))
            ?? throw new Exception($"No soft-deleted Book with ID '{id}' found.");

        book.IsDeleted = false;
        await bookRepository.SaveChangesAsync();

        return Ok(book);
    }
}
