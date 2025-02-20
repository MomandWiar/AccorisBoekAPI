using Infrastructure.Repositories.Book;
using Domain.Entities;
using MediatR;

namespace Application.Features.Books.Queries;

public sealed class GetBookByIdQueryHandler(IBookRepository _bookRepository) : IRequestHandler<GetBookByIdQuery, BookEntity>
{
    public async Task<BookEntity> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(request.Id);

        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        return book;
    }
}