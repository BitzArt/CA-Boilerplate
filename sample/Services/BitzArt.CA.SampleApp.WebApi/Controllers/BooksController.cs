using BitzArt.CA.SampleApp.Core;
using Microsoft.AspNetCore.Mvc;

namespace BitzArt.CA.SampleApp;

[Route("books")]
public class BooksController(IBookRepository bookRepository) : ControllerBase
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
        var book = await bookRepository.GetAsync(chats => chats.Where(x => x.Id == id));

        if (book is null) throw new Exception($"Book with ID {id} was not found.");

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] Book book)
    {
        bookRepository.Add(book);
        await bookRepository.SaveChangesAsync();

        return Ok(book);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var book = await bookRepository.GetAsync(chats => chats.Where(x => x.Id == id));

        if (book is null) throw new Exception($"Book with ID {id} was not found.");

        bookRepository.Remove(book);
        await bookRepository.SaveChangesAsync();

        return Ok();
    }
}
