using Infrastructure.Repositories.Book;
using MediatR;
using Application.DTOs;
namespace Application.Features.Books.Queries;

public sealed class GetBookByIdQueryHandler(IBookRepository _bookRepository) : IRequestHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(request.Id);

        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        var bookDto = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            AuthorId = book.AuthorId,
            AuthorName = book.Author.Name
        };

        return bookDto;
    }
}