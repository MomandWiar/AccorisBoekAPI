using Infrastructure.Repositories.Book;
using MediatR;

namespace Application.Features.Books.Commands;

public sealed class UpdateBookCommandHandler(IBookRepository _bookRepository) : IRequestHandler<UpdateBookCommand, Unit>
{
    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetById(request.Id);

        if (book == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        book.Title = request.Book.Title;
        book.AuthorId = request.Book.AuthorId;

        await _bookRepository.Update(book);

        return Unit.Value;
    }
}
