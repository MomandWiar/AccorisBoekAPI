using Application.Features.Authors.Commands;
using Application.Features.Authors.Queries;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Repositories.Author;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController(ISender _sender, IAuthorRepository _authorRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorEntity>>> GetAuthor()
    {
        try
        {
            return await _authorRepository.Get();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Whoops, something went wrong", message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorEntity>> GetAuthorById(int id)
    {
        try
        {
            var author = await _sender.Send(new GetAuthorByIdQuery(id));

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, [FromBody] UpdateAuthorCommand command)
    {
        if (id != command.Id) return BadRequest("ID mismatch between URL and body.");

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
    public async Task<ActionResult<AuthorEntity>> PostAuthor([FromBody] CreateAuthorCommand command)
    {
        try
        {
            var authorId = await _sender.Send(command);

            var createdAuthor = await _authorRepository.GetById(authorId);

            if (createdAuthor == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetAuthorById), new { id = createdAuthor.Id }, createdAuthor);
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { error = "Validation failed", message = ex.Errors.Select(e => e.ErrorMessage) });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Whoops, something went wrong", message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        try
        {
            await _sender.Send(new DeleteAuthorCommand(id));
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}