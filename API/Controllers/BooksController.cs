using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Application.Features.Books.Commands;
using Application.Features.Books.Queries;
using Infrastructure.Repositories.Book;
using MediatR;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(ISender _sender, IBookRepository _bookRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookEntity>>> GetBook()
    {
        return await _bookRepository.Get();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookEntity>> GetBookById(int id)
    {
        var book = await _sender.Send(new GetBookByIdQuery(id));

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID is URL and Body must match");
        }

        try
        {
            await _sender.Send(command);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<BookEntity>> PostBook([FromBody] CreateBookCommand command)
    {
        try
        {
            var bookId = await _sender.Send(command);

            var createdBook = await _bookRepository.GetById(bookId);

            if (createdBook == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            await _sender.Send(new DeleteBookCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}